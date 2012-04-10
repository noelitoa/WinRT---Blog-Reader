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

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace NoelBlogReader
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class ItemsPage : NoelBlogReader.Common.LayoutAwarePage
    {
        public ItemsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property provides the collection of items to be displayed.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            FeedDataSource _feedDataSource = App.DataSource;
            if (_feedDataSource.Feeds.Count == 0)
            {
                await _feedDataSource.GetFeedsAsync();
            }

            this.DefaultViewModel["Items"] = _feedDataSource.Feeds;
        }

        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Frame.Navigate(typeof(SplitPage), e.ClickedItem);
        }

       

        
    }
}
