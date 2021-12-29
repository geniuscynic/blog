
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using XjjXmm.FrameWork.LogExtension;
using Admin.Service.Document.Input;
using Admin.Service.Document.Output;
using Admin.Repository.Document;

namespace Admin.Service.Document
{
    [ProcessLog]
    public interface IDocumentService
    {
        Task<DocumentEntity> Get(long id);

        Task<List<string>> GetImageList(long id);

        Task<DocumentGetGroupOutput> GetGroup(long id);

        Task<DocumentGetMenuOutput> GetMenu(long id);

        Task<DocumentGetContentOutput> GetContent(long id);

        Task<object> GetPlainList();

        Task<List<DocumentListOutput>> GetList(string key, DateTime? start, DateTime? end);

        Task<bool> AddGroup(DocumentAddGroupInput input);

        Task<bool> AddMenu(DocumentAddMenuInput input);

        Task<bool> AddImage(DocumentAddImageInput input);

        Task<bool> UpdateGroup(DocumentUpdateGroupInput input);

        Task<bool> UpdateMenu(DocumentUpdateMenuInput input);

        Task<bool> UpdateContent(DocumentUpdateContentInput input);

        Task<bool> Delete(long id);

        Task<bool> DeleteImage(long documentId, string url);

        Task<bool> SoftDelete(long id);
    }
}