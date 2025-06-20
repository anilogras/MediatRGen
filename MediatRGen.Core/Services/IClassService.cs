﻿using MediatRGen.Core.Base;
using MediatRGen.Core.Models;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Services
{
    public interface IClassService
    {
        public ServiceResult<bool> ReWriteClass(string classPath, SyntaxNode newRoot);
        public ServiceResult<SyntaxNode> GetClassRoot(string classPath);
        public Task<ServiceResult<bool>> CreateClass(List<ClassConfiguration> classSettings);
        public ServiceResult<SyntaxNode> AddClassAttribute(SyntaxNode root, string attributeName, params string[] arguments);
    }
}
