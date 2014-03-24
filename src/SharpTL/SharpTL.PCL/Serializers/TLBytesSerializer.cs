﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TLBytesSerializer.cs">
//   Copyright (c) 2013 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace SharpTL.Serializers
{
    public class TLBytesSerializer : TLBareTypeSerializerBase
    {
        private static readonly Type _SupportedType = typeof (byte[]);

        private readonly bool _isDurovMode;

        public TLBytesSerializer() : this(false)
        {
        }

        public TLBytesSerializer(bool isDurovMode)
        {
            _isDurovMode = isDurovMode;
        }

        /// <summary>
        ///     In Durov mode Bytes is an alias for String type hence both serializers have the same constructor numbers.
        /// </summary>
        public bool IsDurovMode
        {
            get { return _isDurovMode; }
        }

        public override Type SupportedType
        {
            get { return _SupportedType; }
        }

        public override uint ConstructorNumber
        {
            // string#B5286E24 ? = String;
            // bytes#EBEFB69E ? = Bytes;
            get { return _isDurovMode ? 0xB5286E24 : 0xEBEFB69E; }
        }

        protected override object ReadBody(TLSerializationContext context)
        {
            return context.Streamer.ReadTLBytes();
        }

        protected override void WriteBody(object obj, TLSerializationContext context)
        {
            context.Streamer.WriteTLBytes((byte[]) obj);
        }
    }
}
