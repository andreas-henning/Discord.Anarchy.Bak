using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;

namespace Discord
{
    public class ImageJsonConverter : JsonConverter<DiscordImage>
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override DiscordImage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, DiscordImage value, JsonSerializerOptions options)
        {
            writer.WriteRawValue(value.ToString());
        }
    }

    public enum ImageType
    {
        Png,
        Gif,
        Jpeg,
    }

    [JsonConverter(typeof(ImageJsonConverter))]
    public class DiscordImage
    {
        public PlatformImage Image { get; }

        public ImageType Type { get; }

        public DiscordImage(IImage image, ImageType type)
        {
            if (image != null)
            {
                Image = image.ToPlatformImage() as PlatformImage;
                Type = type;
            }
        }

        public DiscordImage(byte[] bytes, ImageType type)
        {
            if (bytes.Length > 0)
            {
                Image = PlatformImage.FromStream(new MemoryStream(bytes)) as PlatformImage;
                Type = type;
            }
        }

        public override string ToString()
        {
            if (Image == null)
                return null;

            string type = Type switch
            {
                ImageType.Jpeg => "jpeg",
                ImageType.Png => "png",
                ImageType.Gif => "gif",
                _ => throw new NotSupportedException("File extension not supported")
            };

            return $"data:image/{type};base64,{Convert.ToBase64String(Image.Bytes)}";
        }
    }
}
