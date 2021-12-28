
using Admin.Repository.Document;
using Admin.Repository.DocumentImage;
using Admin.Service.Document.Input;
using Admin.Service.Document.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XjjXmm.FrameWork.DependencyInjection;

namespace Admin.Service.Document
{
    [Injection]
    public class DocumentService : BaseService, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentImageRepository _documentImageRepository;

        public DocumentService(
            IDocumentRepository DocumentRepository,
            IDocumentImageRepository documentImageRepository
        )
        {
            _documentRepository = DocumentRepository;
            _documentImageRepository = documentImageRepository;
        }

        public async Task<DocumentEntity> GetAsync(long id)
        {
            var result = await _documentRepository.GetAsync(id);

            return result;
        }

        public async Task<DocumentGetGroupOutput> GetGroupAsync(long id)
        {
            var result = await _documentRepository.GetAsync<DocumentGetGroupOutput>(id);
            return result;
        }

        public async Task<DocumentGetMenuOutput> GetMenuAsync(long id)
        {
            var result = await _documentRepository.GetAsync<DocumentGetMenuOutput>(id);
            return result;
        }

        public async Task<DocumentGetContentOutput> GetContentAsync(long id)
        {
            var result = await _documentRepository.GetAsync<DocumentGetContentOutput>(id);
            return result;
        }

        public async Task<List<DocumentListOutput>> GetListAsync(string key, DateTime? start, DateTime? end)
        {
            if (end.HasValue)
            {
                end = end.Value.AddDays(1);
            }

            var data = await _documentRepository
                .WhereIf(key.NotNull(), a => a.Name.Contains(key) || a.Label.Contains(key))
                .WhereIf(start.HasValue && end.HasValue, a => a.CreatedTime.Value.BetweenEnd(start.Value, end.Value))
                .OrderBy(a => a.ParentId)
                .OrderBy(a => a.Sort)
                .ToListAsync<DocumentListOutput>();

            return data;
        }

        public async Task<List<string>> GetImageListAsync(long id)
        {
            var result = await _documentImageRepository.Select
                .Where(a => a.DocumentId == id)
                .OrderByDescending(a => a.Id)
                .ToListAsync(a => a.Url);

            return result;
        }

        public async Task<bool> AddGroupAsync(DocumentAddGroupInput input)
        {
            var entity = Mapper.Map<DocumentEntity>(input);
            var id = (await _documentRepository.InsertAsync(entity)).Id;

            return id > 0;
        }

        public async Task<bool> AddMenuAsync(DocumentAddMenuInput input)
        {
            var entity = Mapper.Map<DocumentEntity>(input);
            var id = (await _documentRepository.InsertAsync(entity)).Id;

            return id > 0;
        }

        public async Task<bool> AddImageAsync(DocumentAddImageInput input)
        {
            var entity = Mapper.Map<DocumentImageEntity>(input);
            var id = (await _documentImageRepository.InsertAsync(entity)).Id;

            return id > 0;
        }

        public async Task<bool> UpdateGroupAsync(DocumentUpdateGroupInput input)
        {
            var result = false;
            if (input != null && input.Id > 0)
            {
                var entity = await _documentRepository.GetAsync(input.Id);
                entity = Mapper.Map(input, entity);
                result = await _documentRepository.UpdateAsync(entity) > 0;
            }

            return result;
        }

        public async Task<bool> UpdateMenuAsync(DocumentUpdateMenuInput input)
        {
            var result = false;
            if (input != null && input.Id > 0)
            {
                var entity = await _documentRepository.GetAsync(input.Id);
                entity = Mapper.Map(input, entity);
                result = await _documentRepository.UpdateAsync(entity) > 0;
            }

            return result;
        }

        public async Task<bool> UpdateContentAsync(DocumentUpdateContentInput input)
        {
            var result = false;
            if (input != null && input.Id > 0)
            {
                var entity = await _documentRepository.GetAsync(input.Id);
                entity = Mapper.Map(input, entity);
                result = await _documentRepository.UpdateAsync(entity) > 0;
            }

            return result;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var result = false;
            if (id > 0)
            {
                result = await _documentRepository.DeleteAsync(m => m.Id == id) > 0;
            }

            return result;
        }

        public async Task<bool> DeleteImageAsync(long documentId, string url)
        {
            var result = false;
            if (documentId > 0 && url.NotNull())
            {
                result = await _documentImageRepository.DeleteAsync(m => m.DocumentId == documentId && m.Url == url) > 0;
            }

            return result;
        }

        public async Task<bool> SoftDeleteAsync(long id)
        {
            var result = await _documentRepository.SoftDeleteAsync(id);
            return result;
        }

        public async Task<object> GetPlainListAsync()
        {
            var documents = await _documentRepository.Select
                .OrderBy(a => a.ParentId)
                .OrderBy(a => a.Sort)
                .ToListAsync(a => new { a.Id, a.ParentId, a.Label, a.Type, a.Opened });

            var menus = documents
                .Where(a => (new[] { DocumentType.Group, DocumentType.Markdown }).Contains(a.Type))
                .Select(a => new
                {
                    a.Id,
                    a.ParentId,
                    a.Label,
                    a.Type,
                    a.Opened
                });

            return menus;
        }
    }
}