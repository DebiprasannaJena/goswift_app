using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections.ObjectModel;

public partial class includes_PagingUserControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int PreviousIndex { get; set; }
    public int CurrentClickedIndex { get; set; }


    public event EventHandler PaginationLinkClicked;

    protected void LinkButton_Click(object sender, EventArgs e)
    {
        //Assumption: Text of the LinkButton will be same as index
        LinkButton clickedLinkButton = (LinkButton)sender;

        if (String.Equals(clickedLinkButton.Text, "Next"))
        {
            //Next Page index will be one greater than current
            //Note: If the current index is the last page, "Next" control will be in disabled state
            CurrentClickedIndex = PreviousIndex + 1;
        }
        else if (String.Equals(clickedLinkButton.Text, "Prev"))
        {
            //Previous Page index will be one less than current
            //Note: If the current index is the first page, "Prev" control will be in disabled state
            CurrentClickedIndex = PreviousIndex - 1;
        }
        else
        {
            CurrentClickedIndex = Convert.ToInt32(clickedLinkButton.Text, CultureInfo.InvariantCulture);
        }

        //Raise event
        if (this.PaginationLinkClicked != null)
        {
            this.PaginationLinkClicked(clickedLinkButton, e);
        }

    }

    public void PreAddAllLinks(int tableDataCount, int pageSize, int currentIndex)
    {
        if (tableDataCount > 0)
        {
            PagingInfo info = PagingHelper.GetAllLinks(tableDataCount, pageSize, currentIndex);

            //Remove all controls from the placeholder
            plhDynamicLink.Controls.Clear();

            if (info.PaginationLinks != null)
            {
                foreach (LinkButton link in info.PaginationLinks)
                {
                    //Adding Event handler must be done inside Page_Laod /Page_Init
                    link.Click += new EventHandler(LinkButton_Click);

                    //Validation controls should be executed before link click.
                    link.ValidationGroup = "Search";
                    this.plhDynamicLink.Controls.Add(link);
                }
            }

        }
    }

    public void AddPageLinks(int tableDataCount, int pageSize, int index)
    {

        if (tableDataCount > 0)
        {
            pagingSection.Visible = true;
            PagingInfo info = PagingHelper.GetPageLinks(tableDataCount, pageSize, index);

            //Remove all controls from the placeholder
            plhDynamicLink.Controls.Clear();

            if (info.PaginationLinks != null)
            {
                lnkPrevious.Visible = info.PaginationLinks.Count > 0 ? true : false;
                lnkNext.Visible = info.PaginationLinks.Count > 0 ? true : false;

                foreach (LinkButton link in info.PaginationLinks)
                {
                    //Validation controls should be executed before link click.
                    link.ValidationGroup = "Search";
                    this.plhDynamicLink.Controls.Add(link);
                }
            }


            //Dots visiblity
            if (info.IsEndDotsVisible != null)
            {
                lblSecondDots.Visible = Convert.ToBoolean(info.IsEndDotsVisible, CultureInfo.InvariantCulture);
            }
            else
            {
                lblSecondDots.Visible = false;
            }

            if (info.IsStartDotsVisible != null)
            {
                lblFirstDots.Visible = Convert.ToBoolean(info.IsStartDotsVisible, CultureInfo.InvariantCulture);
            }
            else
            {
                lblFirstDots.Visible = false;
            }

            //First and Last Links
            if (info.IsFirstLinkVisible != null)
            {
                lnkFirst.Visible = Convert.ToBoolean(info.IsFirstLinkVisible, CultureInfo.InvariantCulture);
            }
            else
            {
                lnkFirst.Visible = false;
            }

            if (info.IsLastLinkVisible != null)
            {
                lnkLast.Visible = Convert.ToBoolean(info.IsLastLinkVisible, CultureInfo.InvariantCulture);
                lnkLast.Text = info.NumberOfPagesRequired.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                lnkLast.Visible = false;
            }


            //For first page, there is no previous
            if (index != 1 && info.NumberOfPagesRequired != 1)
            {
                lnkPrevious.Enabled = true;
            }
            else
            {
                lnkPrevious.Enabled = false;
            }


            //For last page there is no Next
            if (index != info.NumberOfPagesRequired && info.NumberOfPagesRequired != 1)
            {
                lnkNext.Enabled = true;
            }
            else
            {
                lnkNext.Enabled = false;
            }
        }
        else
        {
            pagingSection.Visible = false;
        }

    }

}

public class PagingInfo
{
    public Collection<LinkButton> PaginationLinks { get; set; }
    public bool? IsEndDotsVisible { get; set; }
    public bool? IsStartDotsVisible { get; set; }
    public bool? IsFirstLinkVisible { get; set; }
    public bool? IsLastLinkVisible { get; set; }
    public int NumberOfPagesRequired { get; set; }

}
public static class PagingHelper
{
    public static PagingInfo GetAllLinks(int totalRecordsInTable, int pageSize, int previousIndex)
    {

        string LinkButtonIDPrefix = "lnK";
        PagingInfo pagingInfo = new PagingInfo();
        pagingInfo.PaginationLinks = new Collection<LinkButton>();

        if (totalRecordsInTable > 0)
        {
            int itemsBeforePage = 4;
            int itemsAfterPage = 2;
            int dynamicDisplayCount = itemsBeforePage + 1 + itemsAfterPage;

            Double numberOfPagesRequired = Convert.ToDouble(totalRecordsInTable / pageSize);
            if (totalRecordsInTable % pageSize != 0)
            {
                numberOfPagesRequired = numberOfPagesRequired + 1;
            }

            if (numberOfPagesRequired == 0)
            {
                numberOfPagesRequired = 1;
            }


            //Note: This function adds only the probable Links that the user can click (based on previous click).
            //This is needed sice dynamic controls need to be added while Page_Load itself for event handlers to work
            //In case of any bug, easiest way is add all links from 1 to numberOfPagesRequired
            //Following is an optimized way

            int endOfLeftPart = dynamicDisplayCount;
            //User may click "1". So the first 7 items may be required for display. Hence add them for event handler purpose
            for (int i = 1; i <= endOfLeftPart; i++)
            {
                //Create dynamic Links 
                LinkButton lnk = new LinkButton();
                lnk.ID = LinkButtonIDPrefix + i.ToString(CultureInfo.InvariantCulture);
                lnk.Text = i.ToString(CultureInfo.InvariantCulture);
                pagingInfo.PaginationLinks.Add(lnk);
            }


            int startOfRighPart = Convert.ToInt32(numberOfPagesRequired) - dynamicDisplayCount + 1;

            //User may click the last link. So the last 7 items may be required for display. Hence add them for event handler purpose
            for (int i = startOfRighPart; i <= Convert.ToInt32(numberOfPagesRequired); i++)
            {
                //Links already added should not be added again
                if (i > endOfLeftPart)
                {
                    //Create dynamic Links 
                    LinkButton lnk = new LinkButton();
                    lnk.ID = LinkButtonIDPrefix + i.ToString(CultureInfo.InvariantCulture);
                    lnk.Text = i.ToString(CultureInfo.InvariantCulture);
                    pagingInfo.PaginationLinks.Add(lnk);
                }
            }

            //User may click on 4 items before current index as well as 2 items after current index
            for (int i = (previousIndex - itemsBeforePage); i <= (previousIndex + itemsAfterPage); i++)
            {
                //Links already added should not be added again
                if (i > endOfLeftPart && i < startOfRighPart)
                {
                    //Create dynamic Links 
                    LinkButton lnk = new LinkButton();
                    lnk.ID = LinkButtonIDPrefix + i.ToString(CultureInfo.InvariantCulture);
                    lnk.Text = i.ToString(CultureInfo.InvariantCulture);
                    pagingInfo.PaginationLinks.Add(lnk);
                }
            }



        }
        return pagingInfo;
    }
    public static PagingInfo GetPageLinks(int totalRecordsInTable, int pageSize, int currentIndex)
    {
        string LinkButtonIDPrefix = "lnK";
        PagingInfo pagingInfo = new PagingInfo();
        pagingInfo.PaginationLinks = new Collection<LinkButton>();

        if (totalRecordsInTable > 0)
        {

            int itemsBeforePage = 4;
            int itemsAfterPage = 2;
            int dynamicDisplayCount = itemsBeforePage + 1 + itemsAfterPage;

            Double numberOfPagesRequired = Convert.ToDouble(totalRecordsInTable / pageSize);
            if (totalRecordsInTable % pageSize != 0)
            {
                numberOfPagesRequired = numberOfPagesRequired + 1;
            }

            if (numberOfPagesRequired == 0)
            {
                numberOfPagesRequired = 1;
            }

            //Generate dynamic paging 
            int start;
            if (currentIndex <= (itemsBeforePage + 1))
            {
                start = 1;
            }
            else
            {
                start = currentIndex - itemsBeforePage;
            }

            int lastAddedLinkIndex = 0;
            int? firtsAddedLinkIndex = null;

            for (int i = start; i < start + dynamicDisplayCount; i++)
            {

                if (i > numberOfPagesRequired)
                {
                    break;
                }

                //Create dynamic Links 
                LinkButton lnk = new LinkButton();
                lnk.ID = LinkButtonIDPrefix + i.ToString(CultureInfo.InvariantCulture);
                lnk.Text = i.ToString(CultureInfo.InvariantCulture);
                lastAddedLinkIndex = i;

                if (firtsAddedLinkIndex == null)
                {
                    firtsAddedLinkIndex = i;
                }


                //Check whetehr current page
                if (i == currentIndex)
                {
                    lnk.CssClass = "page-numbers current";
                }
                else
                {
                    lnk.CssClass = "page-numbers";
                }

                pagingInfo.PaginationLinks.Add(lnk);
            }

            if (numberOfPagesRequired > dynamicDisplayCount)
            {
                //Set dots (ellipse) visibility
                pagingInfo.IsEndDotsVisible = lastAddedLinkIndex == numberOfPagesRequired ? false : true;
                pagingInfo.IsStartDotsVisible = firtsAddedLinkIndex <= 2 ? false : true;

                //First and Last Page Links
                pagingInfo.IsLastLinkVisible = lastAddedLinkIndex == numberOfPagesRequired ? false : true;
                pagingInfo.IsFirstLinkVisible = firtsAddedLinkIndex == 1 ? false : true;

            }

            pagingInfo.NumberOfPagesRequired = Convert.ToInt32(numberOfPagesRequired);

        }
        return pagingInfo;

    }
}