using System;
using FlickrNet;

namespace FlickrSearch
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage is FlickrSearch< search term > ");
    
                Console.WriteLine();
                Console.WriteLine("EG:");
                Console.WriteLine("FlickrSearch \"Polar Bear\"");

                return;
            }

            const string apiKey = "bdbaa5160789362df93b714062c67985";

            Flickr flickr = new Flickr(apiKey);

            string searchTerm = args[0];

            var options = new PhotoSearchOptions
            {
                Tags = searchTerm,
                PerPage = 20,
                Page = 1
            };
            PhotoCollection photos = flickr.PhotosSearch(options);

            var line = 1;
            foreach (Photo photo in photos)
            {
                Console.WriteLine("{3} - Photo {0} has title '{1}' and is at { 2}", photo.PhotoId, photo.Title,
                  photo.LargeUrl, line);
                line++;
            }

        }
    }
}