Example to parse received metadata as a standard .NET DataSet:
```c#
        protected override void ReceivedMetadata(ByteBuffer payload)
        {
            byte[] payloadBytes = payload.ToArray();

            StatusMessage($"Received {payloadBytes.Length:N0} bytes of metadata, parsing...");

            if (MetadataCompressed)
                payloadBytes = Decompress(payloadBytes);

            XmlReader reader = XmlReader.Create(new MemoryStream(payloadBytes));
            System.Data.DataSet dataset = new System.Data.DataSet();
            dataset.ReadXml(reader);

            StatusMessage($"Parsed .NET data set with {dataset.Tables.Count:N0} tables from received XML metadata payload.");
            
            // Provide to base class only if native metadata structures are needed
            // base.ReceivedMetadata(payload);
        }

        private static byte[] Decompress(byte[] gzip)
        {
            using GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress, false);
            using MemoryStream memory = new MemoryStream();

            stream.CopyTo(memory);

            return memory.ToArray();
        }
```
