using System.Collections.Generic;
using HotChocolate.Types;

namespace StrawberryShake.CodeGeneration
{
    /// <summary>
    /// Describes a GraphQl subscription
    /// </summary>
    public class SubscriptionOperationDescriptor: OperationDescriptor
    {
        public override string Name => NamingConventions.SubscriptionServiceNameFromTypeName(ResultTypeReference.Name);

        public SubscriptionOperationDescriptor(
            ITypeDescriptor resultTypeReference,
            IReadOnlyList<NamedTypeReferenceDescriptor> arguments) : base(resultTypeReference, arguments)
        {
        }
    }
}