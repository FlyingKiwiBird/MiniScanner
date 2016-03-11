//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="EmploymentHistoryListbox.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveScanner.UI
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    using EveOnlineApi.Entities;
    using EveOnlineApi.Interfaces;
    using EveScanner.IoC;

    /// <summary>
    /// Renders an Employment History record with data from the EVE API
    /// </summary>
    public class EmploymentHistoryListbox : ListBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmploymentHistoryListbox"/> class.
        /// </summary>
        public EmploymentHistoryListbox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.ItemHeight = 64;
        }

        /// <summary>
        /// Overrides the OnDrawItem method which renders an individual list item.
        /// </summary>
        /// <param name="e">Used to indicate if we're in design mode, and to provide access to the draw methods</param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e == null)
            {
                return;
            }

            const TextFormatFlags Flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;

            if (e.Index >= 0)
            {
                e.DrawBackground();

                if (this.DesignMode)
                {
                    TextRenderer.DrawText(e.Graphics, "Employment History List Box - Design Mode", e.Font, e.Bounds, e.ForeColor, Flags);
                }
                else
                {
                    EmploymentHistoryEntry ehe = this.Items[e.Index] as EmploymentHistoryEntry;

                    if (ehe != null)
                    {
                        Rectangle bounds = e.Bounds;

                        StringBuilder sb = new StringBuilder();

                        if (ehe.Corporation != null)
                        {
                            using (Image i = EmploymentHistoryListbox.GetImage("Corporation", ehe.CorporationId, 64))
                            {
                                e.Graphics.DrawImage(i, bounds.X, bounds.Y, i.Width, i.Height);
                                bounds.X += i.Width;
                                bounds.Width -= i.Width;
                            }

                            sb.AppendFormat("{0} [{1}]", ehe.Corporation.Name, ehe.Corporation.Ticker);

                            if (ehe.Corporation.Alliance != null)
                            {
                                using (Image i = EmploymentHistoryListbox.GetImage("Alliance", ehe.Corporation.AllianceId, 64))
                                {
                                    e.Graphics.DrawImage(i, bounds.X, bounds.Y, i.Width, i.Height);
                                    bounds.X += i.Width;
                                    bounds.Width -= i.Width;
                                }

                                sb.AppendFormat(" / {0} [{1}]", ehe.Corporation.Alliance.Name, ehe.Corporation.Alliance.ShortName);
                            }
                        }

                        sb.AppendLine();
                        sb.AppendFormat("From {0} to ", ehe.StartDate.ToUniversalTime().ToString());

                        if (ehe.EndDate == DateTime.MaxValue)
                        {
                            sb.Append("Now");
                        }
                        else
                        {
                            sb.Append(ehe.EndDate.ToUniversalTime().ToUniversalTime());
                        }

                        TextRenderer.DrawText(e.Graphics, sb.ToString(), e.Font, bounds, e.ForeColor, Flags);
                    }
                    else
                    {
                        TextRenderer.DrawText(e.Graphics, this.Items[e.Index].ToString(), e.Font, e.Bounds, e.ForeColor, Flags);
                    }
                }

                e.DrawFocusRectangle();
            }
        }

        /// <summary>
        /// Gets an image from the EVE Image Servers.
        /// </summary>
        /// <param name="subDirectory">Which subdirectory/type to get an image for</param>
        /// <param name="id">The ID of the image to retrieve</param>
        /// <param name="width">Width of the image</param>
        /// <returns>Image object</returns>
        private static Image GetImage(string subDirectory, int id, int width)
        {
            IImageDataProvider images = Injector.Create<IImageDataProvider>();

            byte[] data = images.GetImageData(subDirectory, id, width);
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
