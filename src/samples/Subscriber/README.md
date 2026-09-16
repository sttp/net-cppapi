# Subscriber sample

The sample targets .NET 6, .NET 9, and .NET 10. Use the .NET 10 SDK to build and the selected .NET runtime to run.

From the repository root, connect to an STTP publisher:

```powershell
dotnet run --project src/samples/Subscriber -c Release -f net10.0 -p:Platform=x64 -p:GeneratePackageOnBuild=false -- 127.0.0.1 7165
```

Replace the host and port with your publisher's endpoint. Use `-f net9.0` or `-f net6.0` to select another target. Press Enter to exit. See the [build instructions](../../lib/README.md) for native library setup.

## Parse metadata as a .NET DataSet

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
