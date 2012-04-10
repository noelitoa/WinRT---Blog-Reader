

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace NoelBlogReader
{
    // FeedData
    // Holds info for a single blog feed, including a list of blog posts (FeedItem)
    public class FeedData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PubDate { get; set; }

        private List<FeedItem> _Items = new List<FeedItem>();
        public List<FeedItem> Items
        {
            get
            {
                return this._Items;
            }
        }
    }

    // FeedItem
    // Holds info for a single blog post
    public class FeedItem
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime PubDate { get; set; }
        public Uri Link { get; set; }
    }

    // FeedDataSource
    // Holds a collection of blog feeds (FeedData), and contains methods needed to
    // retreive the feeds.
    public class FeedDataSource
    {
        private ObservableCollection<FeedData> _Feeds = new ObservableCollection<FeedData>();
        public ObservableCollection<FeedData> Feeds
        {
            get
            {
                return this._Feeds;
            }
        }

        public async Task GetFeedsAsync()
        {
            Task<FeedData> feed1 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/developers/atom.aspx");
            Task<FeedData> feed2 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/windowsexperience/atom.aspx");
            Task<FeedData> feed3 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/extremewindows/atom.aspx");
            Task<FeedData> feed4 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/business/atom.aspx");
            Task<FeedData> feed5 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/bloggingwindows/atom.aspx");
            Task<FeedData> feed6 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/windowssecurity/atom.aspx");
            Task<FeedData> feed7 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/springboard/atom.aspx");
            Task<FeedData> feed8 =
                GetFeedAsync("http://windowsteamblog.com/windows/b/windowshomeserver/atom.aspx");
            // There is no Atom feed for this blog, so we use the RSS feed.
            Task<FeedData> feed9 =
                GetFeedAsync("http://windowsteamblog.com/windows_live/b/windowslive/rss.aspx");
            Task<FeedData> feed10 =
                GetFeedAsync("http://windowsteamblog.com/windows_live/b/developer/atom.aspx");
            Task<FeedData> feed11 =
                GetFeedAsync("http://windowsteamblog.com/ie/b/ie/atom.aspx");
            Task<FeedData> feed12 =
                GetFeedAsync("http://windowsteamblog.com/windows_phone/b/wpdev/atom.aspx");
            Task<FeedData> feed13 =
                GetFeedAsync("http://windowsteamblog.com/windows_phone/b/wmdev/atom.aspx");

            this.Feeds.Add(await feed1);
            this.Feeds.Add(await feed2);
            this.Feeds.Add(await feed3);
            this.Feeds.Add(await feed4);
            this.Feeds.Add(await feed5);
            this.Feeds.Add(await feed6);
            this.Feeds.Add(await feed7);
            this.Feeds.Add(await feed8);
            this.Feeds.Add(await feed9);
            this.Feeds.Add(await feed10);
            this.Feeds.Add(await feed11);
            this.Feeds.Add(await feed12);
            this.Feeds.Add(await feed13);
        }

        private async Task<FeedData> GetFeedAsync(string feedUriString)
        {
            // using Windows.Web.Syndication;
            SyndicationClient client = new SyndicationClient();
            Uri feedUri = new Uri(feedUriString);

            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                // This code is executed after RetrieveFeedAsync returns the SyndicationFeed.
                // Process it and copy the data we want into our FeedData and FeedItem classes.
                FeedData feedData = new FeedData();

                feedData.Title = feed.Title.Text;
                if (feed.Subtitle.Text != null)
                {
                    feedData.Description = feed.Subtitle.Text;
                }
                // Use the date of the latest post as the last updated date.
                feedData.PubDate = feed.Items[0].PublishedDate.DateTime;

                foreach (SyndicationItem item in feed.Items)
                {
                    FeedItem feedItem = new FeedItem();
                    feedItem.Title = item.Title.Text;
                    feedItem.PubDate = item.PublishedDate.DateTime;
                    feedItem.Author = item.Authors[0].Name.ToString();
                    // Handle the differences between RSS and Atom feeds.
                    if (feed.SourceFormat == SyndicationFormat.Atom10)
                    {
                        feedItem.Content = item.Content.Text;
                        feedItem.Link = new Uri("http://windowsteamblog.com" + item.Id);
                    }
                    else if (feed.SourceFormat == SyndicationFormat.Rss20)
                    {
                        feedItem.Content = item.Summary.Text;
                        feedItem.Link = item.Links[0].Uri;
                    }
                    feedData.Items.Add(feedItem);
                }
                return feedData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}