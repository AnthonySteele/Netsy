//-----------------------------------------------------------------------
// <copyright file="JsonSerializerHelper.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    /// <summary>
    /// Helper methods for deserialising Json
    /// </summary>
    public static class JsonSerializerHelper
    {
        /// <summary>
        /// Cache of serialisers for types
        /// </summary>
        private static readonly Dictionary<Type, DataContractJsonSerializer> seralizerCache = new Dictionary<Type, DataContractJsonSerializer>();

        /// <summary>
        /// Turn json text into the matching object
        /// </summary>
        /// <typeparam name="T">the object type</typeparam>
        /// <param name="jsonData">json text data</param>
        /// <returns>the object converted from the data</returns>
        public static T Deserialize<T>(this string jsonData) where T : class
        {
            DataContractJsonSerializer serializer = GetSerializer(typeof(T));

            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonData));
            return serializer.ReadObject(ms) as T;
        }

        /// <summary>
        /// Get the serialiser for the type
        /// </summary>
        /// <param name="type">the type to deserialise into</param>
        /// <returns>the Datacontract json serialiser</returns>
        private static DataContractJsonSerializer GetSerializer(Type type)
        {
            if (!seralizerCache.ContainsKey(type))
            {
                seralizerCache[type] = new DataContractJsonSerializer(type);
            }

            return seralizerCache[type];
        }
    }
}
