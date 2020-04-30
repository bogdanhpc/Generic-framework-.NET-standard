namespace Core
{
    public static class KnownContentTypesExtensions
    {
        public static string ToMimeString(this KnownContentSerializers serializer)
        {
            switch (serializer)
            {
                case KnownContentSerializers.Json:
                    return "application/json";

                case KnownContentSerializers.Xml:
                    return "application/xml";

                default:
                    return "application/octet.stream";
            }
        }
    }
}
