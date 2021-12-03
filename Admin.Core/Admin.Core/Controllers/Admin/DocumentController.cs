﻿using Admin.Core.Common.Configs;
using Admin.Core.Common.Helpers;
using Admin.Core.Common.Output;
using Admin.Core.Service.Admin.Document;
using Admin.Core.Service.Admin.Document.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Core.Service.Admin.Document.Output;
using XjjXmm.FrameWork.Common;

namespace Admin.Core.Controllers.Admin
{
    /// <summary>
    /// 文档管理
    /// </summary>
    public class DocumentController : AreaController
    {
        private readonly IDocumentService _documentService;
        private readonly UploadConfig _uploadConfig;
        private readonly UploadHelper _uploadHelper;

        public DocumentController(
            UploadHelper uploadHelper,
            IOptionsMonitor<UploadConfig> uploadConfig,
            IDocumentService documentService
        )
        {
            _uploadHelper = uploadHelper;
            _uploadConfig = uploadConfig.CurrentValue;
            _documentService = documentService;
        }

        /// <summary>
        /// 查询文档列表
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<DocumentListOutput>> GetList(string key, DateTime? start, DateTime? end)
        {
            return await _documentService.GetListAsync(key, start, end);
        }

        /// <summary>
        /// 查询文档图片列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<string>> GetImageList(long id)
        {
            return await _documentService.GetImageListAsync(id);
        }

        /// <summary>
        /// 查询单条分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DocumentGetGroupOutput> GetGroup(long id)
        {
            return await _documentService.GetGroupAsync(id);
        }

        /// <summary>
        /// 查询单条菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DocumentGetMenuOutput> GetMenu(long id)
        {
            return await _documentService.GetMenuAsync(id);
        }

        /// <summary>
        /// 查询单条文档内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<DocumentGetContentOutput> GetContent(long id)
        {
            return await _documentService.GetContentAsync(id);
        }

        /// <summary>
        /// 查询精简文档列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetPlainList()
        {
            return await _documentService.GetPlainListAsync();
        }

        /// <summary>
        /// 新增分组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddGroup(DocumentAddGroupInput input)
        {
            return await _documentService.AddGroupAsync(input);
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AddMenu(DocumentAddMenuInput input)
        {
            return await _documentService.AddMenuAsync(input);
        }

        /// <summary>
        /// 修改分组
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateGroup(DocumentUpdateGroupInput input)
        {
            return await _documentService.UpdateGroupAsync(input);
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateMenu(DocumentUpdateMenuInput input)
        {
            return await _documentService.UpdateMenuAsync(input);
        }

        /// <summary>
        /// 修改文档内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<bool> UpdateContent(DocumentUpdateContentInput input)
        {
            return await _documentService.UpdateContentAsync(input);
        }

        /// <summary>
        /// 删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> SoftDelete(long id)
        {
            return await _documentService.SoftDeleteAsync(id);
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> DeleteImage(long documentId, string url)
        {
            return await _documentService.DeleteImageAsync(documentId, url);
        }

        /// <summary>
        /// 上传文档图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> UploadImage([FromForm] DocumentUploadImageInput input)
        {
            var config = _uploadConfig.Document;
            var res = await _uploadHelper.UploadAsync(input.File, config, new { input.Id });
            //if (res.Success)
            //{
                //保存文档图片
                var r = await _documentService.AddImageAsync(
                new DocumentAddImageInput
                {
                    DocumentId = input.Id,
                    Url = res.FileRequestPath
                });
                if (r)
                {
                    return res.FileRequestPath;
                }
            //}

            throw new BussinessException(StatusCodes.Status999Falid, "上传失败！");
            //return "";
            //return ResponseOutput.NotOk("上传失败！");
        }
    }
}