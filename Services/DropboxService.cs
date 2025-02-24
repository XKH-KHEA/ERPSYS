using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ERPSYS.Services
{

    public class DropboxService
    {
        private readonly string _accessToken;

        public DropboxService(IConfiguration configuration)
        {
            _accessToken = configuration["Dropbox:AccessToken"] ?? throw new ArgumentNullException(nameof(configuration), "Dropbox access token is not configured.");
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            using (var dbx = new DropboxClient(_accessToken))
            {
                string dropboxPath = $"/uploads/{fileName}";

                await dbx.Files.UploadAsync(
                    dropboxPath,
                    WriteMode.Overwrite.Instance,
                    body: fileStream
                );
                var sharedLink = await GetSharedLinkAsync(dbx, dropboxPath);
                return sharedLink;
                //return dropboxPath; // Returning Dropbox file path
            }
        }
        private async Task<string> GetSharedLinkAsync(DropboxClient dbx, string dropboxPath)
        {
            try
            {
                // Check if a shared link already exists
                var sharedLinks = await dbx.Sharing.ListSharedLinksAsync(dropboxPath);
                if (sharedLinks.Links.Count > 0)
                {
                    return ConvertToDirectUrl(sharedLinks.Links[0].Url);
                }

                // Create a new shared link
                var newLink = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(dropboxPath);
                return ConvertToDirectUrl(newLink.Url);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating shared link: {ex.Message}");
            }
        }

        private string ConvertToDirectUrl(string dropboxUrl)
        {
            return dropboxUrl.Replace("?dl=0", "?raw=1"); // Converts to direct image URL
        }

    }


}
