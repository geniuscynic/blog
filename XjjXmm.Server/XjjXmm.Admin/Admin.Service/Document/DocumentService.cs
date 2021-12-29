
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

        public async Task<DocumentEntity> Get(long id)
        {
            var result = await _documentRepository.GetById(id);

            return result;
        }

        public async Task<DocumentGetGroupOutput> GetGroup(long id)
        {
            //var result = await _documentRepository.GetAsync<DocumentGetGroupOutput>(id);
            //return result;

            return null;
        }

        public async Task<DocumentGetMenuOutput> GetMenu(long id)
        {
            //var result = await _documentRepository.GetAsync<DocumentGetMenuOutput>(id);
            //return result;
            return null;
        }

        public async Task<DocumentGetContentOutput> GetContent(long id)
        {
            //var result = await _documentRepository.GetAsync<DocumentGetContentOutput>(id);
            //return result;
            return null;
        }

        public async Task<List<DocumentListOutput>> GetList(string key, DateTime? start, DateTime? end)
        {
            //if (end.HasValue)
            //{
            //    end = end.Value.AddDays(1);
            //}

            //var data = await _documentRepository
            //    .WhereIf(key.NotNull(), a => a.Name.Contains(key) || a.Label.Contains(key))
            //    .WhereIf(start.HasValue && end.HasValue, a => a.CreatedTime.Value.BetweenEnd(start.Value, end.Value))
            //    .OrderBy(a => a.ParentId)
            //    .OrderBy(a => a.Sort)
            //    .ToListAsync<DocumentListOutput>();

            //return data;

            return null;
        }

        public async Task<List<string>> GetImageList(long id)
        {
            //var result = await _documentImageRepository.Select
            //    .Where(a => a.DocumentId == id)
            //    .OrderByDescending(a => a.Id)
            //    .ToList(a => a.Url);

            //return result;

            return null;
        }

        public async Task<bool> AddGroup(DocumentAddGroupInput input)
        {
            //var entity = Mapper.Map<DocumentEntity>(input);
            //var id = (await _documentRepository.Insert(entity)).Id;

            //return id > 0;

            throw new NotImplementedException();
        }

        public async Task<bool> AddMenu(DocumentAddMenuInput input)
        {
            //var entity = Mapper.Map<DocumentEntity>(input);
            //var id = (await _documentRepository.Insert(entity)).Id;

            //return id > 0;

            throw new NotImplementedException();
        }

        public async Task<bool> AddImage(DocumentAddImageInput input)
        {
            //var entity = Mapper.Map<DocumentImageEntity>(input);
            //var id = (await _documentImageRepository.Insert(entity)).Id;

            //return id > 0;

            throw new NotImplementedException();
        }

        public async Task<bool> UpdateGroup(DocumentUpdateGroupInput input)
        {
            //var result = false;
            //if (input != null && input.Id > 0)
            //{
            //    var entity = await _documentRepository.Get(input.Id);
            //    entity = Mapper.Map(input, entity);
            //    result = await _documentRepository.Update(entity) > 0;
            //}

            //return result;

           throw new NotImplementedException();
        }

        public async Task<bool> UpdateMenu(DocumentUpdateMenuInput input)
        {
            //var result = false;
            //if (input != null && input.Id > 0)
            //{
            //    var entity = await _documentRepository.Get(input.Id);
            //    entity = Mapper.Map(input, entity);
            //    result = await _documentRepository.Update(entity) > 0;
            //}

            //return result;
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateContent(DocumentUpdateContentInput input)
        {
            //var result = false;
            //if (input != null && input.Id > 0)
            //{
            //    var entity = await _documentRepository.Get(input.Id);
            //    entity = Mapper.Map(input, entity);
            //    result = await _documentRepository.Update(entity) > 0;
            //}

            //return result;
            throw new NotImplementedException();

        }

        public async Task<bool> Delete(long id)
        {
            //var result = false;
            //if (id > 0)
            //{
            //    result = await _documentRepository.Delete(m => m.Id == id) > 0;
            //}

            //return result;
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteImage(long documentId, string url)
        {
            //var result = false;
            //if (documentId > 0 && url.NotNull())
            //{
            //    result = await _documentImageRepository.Delete(m => m.DocumentId == documentId && m.Url == url) > 0;
            //}

            //return result;
            throw new NotImplementedException();
        }

        public async Task<bool> SoftDelete(long id)
        {
            var result = await _documentRepository.SoftDelete(id);
            return result;
        }

        public async Task<object> GetPlainList()
        {
            //var documents = await _documentRepository.Select
            //    .OrderBy(a => a.ParentId)
            //    .OrderBy(a => a.Sort)
            //    .ToList(a => new { a.Id, a.ParentId, a.Label, a.Type, a.Opened });

            //var menus = documents
            //    .Where(a => (new[] { DocumentType.Group, DocumentType.Markdown }).Contains(a.Type))
            //    .Select(a => new
            //    {
            //        a.Id,
            //        a.ParentId,
            //        a.Label,
            //        a.Type,
            //        a.Opened
            //    });

            //return menus;
            throw new NotImplementedException();
        }
    }
}