using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Serialization.Newtonsoft
{
    public class NonDefaultConstructorContractResolver : DefaultContractResolver
    {
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            return JsonObjectContractProvider.UsingNonDefaultConstructor(
                base.CreateObjectContract(objectType),
                objectType,
                base.CreateConstructorParameters
            );
        }
    }

}
