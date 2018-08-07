using System.Collections.Generic;

namespace sharedclipboard.Models
{
    public class SharedBoardContentBoard
    {
        public string Id {get;set;}
        public IList<SharedBoardItemBoard> Items {get;set;}

        public SharedBoardContentBoard()
        {
            Items = new List<SharedBoardItemBoard>();
        }
    }

    public class SharedBoardItemBoard
    {
        public enum ItemBoardType : byte
        {
            Text = 0,
            Link = 1,
            Image = 2,
            Video = 3
        }

        /**
         *  Text or Name of file to display in UI
         */
        public string Name { get; set; }

        /**
         *  Short text
         */
        public string ShortText { get; set; }

        public string Path { get; set; }

        public ItemBoardType Type { get; set; }
    }
}