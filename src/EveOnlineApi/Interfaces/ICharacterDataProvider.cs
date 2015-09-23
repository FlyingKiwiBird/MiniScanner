//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICharacterDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces
{
    using EveOnlineApi.Entities;

    /// <summary>
    /// Defines the interface for retrieving Character data.
    /// </summary>
    public interface ICharacterDataProvider
    {
        /// <summary>
        /// Gets the Character Id for a particular Character Name
        /// </summary>
        /// <param name="characterName">Character Name</param>
        /// <returns>Character Id</returns>
        int GetCharacterId(string characterName);

        /// <summary>
        /// Gets the Character Information for a particular Character Id.
        /// </summary>
        /// <param name="characterId">Character Id</param>
        /// <returns>Character Info Object</returns>
        Character GetCharacterInfo(int characterId);
    }
}
