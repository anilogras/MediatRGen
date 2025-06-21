using MediatRGen.Cli.Processes.MediatR;
using MediatRGen.Core.Models;
using MediatRGen.Core.Schemas;
using MediatRGen.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatRGen.Core.Concrete.MediatR
{
    public class ControllerService
    {
        private readonly CreateServiceBaseSchema _parameter;
        private readonly ServicePaths _paths;
        IList<ClassConfiguration> _classConfigs;

        private readonly IDirectoryServices _directoryServices;
        private readonly IClassService _classService;
        private readonly INameSpaceService _nameSpaceService;

        public ControllerService(
            CreateServiceBaseSchema parameter,
            ServicePaths paths,
            IList<ClassConfiguration> classConfigs,
            IDirectoryServices directoryServices,
            IClassService classService,
            INameSpaceService nameSpaceService)
        {
            _parameter = parameter;
            _paths = paths;
            _directoryServices = directoryServices;
            _classService = classService;
            _nameSpaceService = nameSpaceService;
            _classConfigs = classConfigs;

        }


        public void CreateController()
        {
            _directoryServices.CreateIsNotExist(_paths.ControllerDirectory);
            ControllerConfiguration();
        }

        private void ControllerConfiguration()
        {
            ClassConfiguration _classConfig = new ClassConfiguration();
            _classConfig.Directory = _paths.ControllerDirectory;
            _classConfig.Name = $"{_paths.EntityName}Controller";
            _classConfig.BaseInheritance = $"Controller";

            _classConfig.Usings = new List<string>
            {
                $"Microsoft.AspNetCore.Mvc" ,
                $"MediatR",
                $"{_paths.ApplicationFeaturesWithEntityNameSpace}.Commands.Create",
                $"{_paths.ApplicationFeaturesWithEntityNameSpace}.Commands.Update",
                $"{_paths.ApplicationFeaturesWithEntityNameSpace}.Commands.Delete",
                $"{_paths.ApplicationFeaturesWithEntityNameSpace}.Queries.GetById",
                $"{_paths.ApplicationFeaturesWithEntityNameSpace}.Queries.GetList",
                $"{_paths.ApplicationFeaturesWithEntityNameSpace}.Queries.GetListDynamic",
                $"{_paths.ApplicationFeaturesWithEntityNameSpace}.Queries.GetListPaged",
            };
            _classConfig.ClassAttr = new List<ClassAttributeConfiguration> {
                
                new ClassAttributeConfiguration {
                   Name =   "ApiController"
                } ,

                new ClassAttributeConfiguration {
                    Name = @"Route(""api/[controller]"")"
                }

            };

            _classConfig.Constructor = true;
            _classConfig.ConstructorParameters = $"IMediator mediator";
            _classConfig.ConstructorCodes = new List<string> { "_mediator = mediator;" };
            _classConfig.ConstructorPrivateFields.Add(new PropertyConfiguration
            {
                Accessibility = Microsoft.CodeAnalysis.CSharp.SyntaxKind.PrivateKeyword,
                FieldName = "_mediator",
                FieldType = "IMediator"
            });

            List<string> _methods = new List<string>();

            _methods.Add($@"
                        [HttpPost]
                        public async Task<IActionResult> Create([FromBody] Create{_paths.EntityName}Command command)
                        {{
                            var result = await _mediator.Send(command);
                            return Ok(result);
                        }}
                ");

            _methods.Add($@"
                        [HttpDelete(""{{id}}"")]
                        public async Task<IActionResult> Delete(Guid id)
                        {{
                            var command = new Delete{_paths.EntityName}Command {{ Id = id }};
                            var result = await _mediator.Send(command);
                            return Ok(result);
                        }}
                ");

            _methods.Add($@"
                            [HttpPut]
                            public async Task<IActionResult> Update([FromBody] Update{_paths.EntityName}Command command)
                            {{
                                var result = await _mediator.Send(command);
                                return Ok(result);
                            }}
                ");

            _methods.Add($@"
                                [HttpGet(""{{id}}"")]
                                public async Task<IActionResult> GetById(Guid id)
                                {{
                                    var result = await _mediator.Send(new GetById{_paths.EntityName}Query {{ Id = id }});
                                    return Ok(result);
                                }}
                ");

            _methods.Add($@"
                                [HttpGet(""List"")]
                                public async Task<IActionResult> GetList()
                                {{
                                    var result = await _mediator.Send(new GetList{_paths.EntityName}Query());
                                    return Ok(result);
                                }}
                ");

            _methods.Add($@"
                                    [HttpPost(""List/Dynamic"")]
                                    public async Task<IActionResult> GetListDynamic([FromBody] GetListDynamic{_paths.EntityName}Query query)
                                    {{
                                        var result = await _mediator.Send(query);
                                        return Ok(result);
                                    }}
                ");

            _methods.Add($@"
                                    [HttpGet(""List/Paged"")]
                                    public async Task<IActionResult> GetListPaged([FromQuery] GetListPaged{_paths.EntityName}Query query)
                                    {{
                                        var result = await _mediator.Send(query);
                                        return Ok(result);
                                    }}
                ");
            _classConfig.Methods = _methods;
            _classConfigs.Add(_classConfig);



        }
    }
}
