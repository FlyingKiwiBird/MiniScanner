//-----------------------------------------------------------------------
// <copyright company="Viktorie Lucilla" file="ICharacterXmlDataProvider.cs">
// Copyright © Viktorie Lucilla 2015. All Rights Reserved
// </copyright>
//-----------------------------------------------------------------------
namespace EveOnlineApi.Interfaces.Xml
{
    using EveOnlineApi.Entities.Xml;

    /// <summary>
    /// Defines the interface for retrieving Eve XML API data for Characters.
    /// </summary>
    public interface ICharacterXmlDataProvider
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
        /// <returns>Character Info XML Object</returns>
        CharacterInfoApi GetCharacterInfo(int characterId);
    }
}
