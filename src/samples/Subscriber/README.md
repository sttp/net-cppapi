Example to parse received metadata as a standard .NET DataSet:
```c#
        protected override void ReceivedMetadata(ByteBuffer payload)
        {
            StatusMessage($"Received {payload.Count} bytes of metadata, parsing...");
            base.ReceivedMetadata(payload);

            byte[] payloadBytes = payload.ToArray();

            if (MetadataCompressed)
                payloadBytes = Decompress(payloadBytes);

            XmlReader reader = XmlReader.Create(new MemoryStream(payloadBytes));

            System.Data.DataSet dataset = new System.Data.DataSet();
            dataset.ReadXml(reader);

            StatusMessage($"Parsed .NET data set with {dataset.Tables.Count:N0} tables from received XML metadata payload.");
        }

        private static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int BufferSize = 4096;
                byte[] buffer = new byte[BufferSize];

                using (MemoryStream memory = new MemoryStream())
                {
                    int count;

                    do
                    {
                        count = stream.Read(buffer, 0, BufferSize);

                        if (count > 0)
                            memory.Write(buffer, 0, count);
                    }
                    while (count > 0);

                    return memory.ToArray();
                }
            }
        }
```
