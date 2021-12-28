
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
        Task<DocumentEntity> GetAsync(long id);

        Task<List<string>> GetImageListAsync(long id);

        Task<DocumentGetGroupOutput> GetGroupAsync(long id);

        Task<DocumentGetMenuOutput> GetMenuAsync(long id);

        Task<DocumentGetContentOutput> GetContentAsync(long id);

        Task<object> GetPlainListAsync();

        Task<List<DocumentListOutput>> GetListAsync(string key, DateTime? start, DateTime? end);

        Task<bool> AddGroupAsync(DocumentAddGroupInput input);

        Task<bool> AddMenuAsync(DocumentAddMenuInput input);

        Task<bool> AddImageAsync(DocumentAddImageInput input);

        Task<bool> UpdateGroupAsync(DocumentUpdateGroupInput input);

        Task<bool> UpdateMenuAsync(DocumentUpdateMenuInput input);

        Task<bool> UpdateContentAsync(DocumentUpdateContentInput input);

        Task<bool> DeleteAsync(long id);

        Task<bool> DeleteImageAsync(long documentId, string url);

        Task<bool> SoftDeleteAsync(long id);
    }
}