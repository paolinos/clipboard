using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using sharedclipboard.helpers;
using sharedclipboard.Models;

namespace sharedclipboard.services
{
    public class SharedBoardService
    {
        private readonly IMemoryCache _cache;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SharedBoardService(IMemoryCache memoryCache, IHostingEnvironment hostingEnv)
        {
            _cache = memoryCache;
            _hostingEnvironment = hostingEnv;
        }

        public string CreateBoard()
        {
            string boardId = "aa";

            var emptyBoard = new SharedBoardContentBoard(){
                Id = boardId
            };

            UpdateBoard(emptyBoard, true);

            return boardId;
        }

        public SharedBoardContentBoard LoadBoard(string boardId)
        {
            SharedBoardContentBoard values = null;
            if(_cache.TryGetValue(boardId, out values)){
                return values;
            }
            return null;
        }

        public bool AddItemsToBoard(FileInputModel uploadBoard)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "assets");

            var board = LoadBoard(uploadBoard.id);
            if(board != null){
                
                bool IsUploadOk = false;
                foreach (var item in uploadBoard.files)
                {
                    var fullpath = FileHelper.UploadFile(uploads, item, ShaHelper.GenerateSHA256String(DateTime.Now.ToString()) );
                    if(fullpath == null){
                        IsUploadOk = false;
                        break;
                    } 

                    board.Items.Add(new SharedBoardItemBoard(){
                        Name = item.FileName,
                        ShortText = "",
                        Path = fullpath,
                        Type = SharedBoardItemBoard.ItemBoardType.Image
                    });

                    IsUploadOk = true;
                }
                if(IsUploadOk){
                    UpdateBoard(board);
                }
            }

            return true;
        }

        private void UpdateBoard(SharedBoardContentBoard board, bool forceUpdateTime = false)
        {
            MemoryCacheEntryOptions cacheExpirationOptions = new MemoryCacheEntryOptions();
            if(forceUpdateTime){
                cacheExpirationOptions.AbsoluteExpiration = DateTime.Now.AddHours(10);
            }
            cacheExpirationOptions.Priority = CacheItemPriority.Normal;

            _cache.Set<SharedBoardContentBoard>(board.Id, board,cacheExpirationOptions);
        }
    }
}