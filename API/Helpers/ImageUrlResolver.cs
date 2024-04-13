using AutoMapper;
using Core.DTOs;
using Core.Models;

namespace API.Helpers
{
    public class ImageUrlResolver : IValueResolver<ImageModel, ImageDto, string>
    {
        private readonly IConfiguration _config;
        public ImageUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(ImageModel source, ImageDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ImagePath))
            {
                return _config["ApiUrl"]! + source.ImagePath;
            }

            #pragma warning disable CS8603 // Possible null reference return.
            return null;
            #pragma warning restore CS8603 // Possible null reference return.
        }
    }
}