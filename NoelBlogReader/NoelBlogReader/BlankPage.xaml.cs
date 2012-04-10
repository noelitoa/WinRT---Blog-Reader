using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace NoelBlogReader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage : Page
    {
        public BlankPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            FeedDataSource _feedDataSource = App.DataSource;
            if (_feedDataSource.Feeds.Count == 0)
            {
                await _feedDataSource.GetFeedsAsync();
            }

            this.DataContext = (_feedDataSource.Feeds).First();
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FeedItem feedItem = e.AddedItems[0] as FeedItem;
            if (feedItem != null)
            {
                ContentView.NavigateToString(feedItem.Content);
            }
        }
    }
}
