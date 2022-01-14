using System.Collections.Generic;
using Microsoft.Extensions.ObjectPool;
using HotChocolate.Execution.Processing;
using HotChocolate.Types;
using HotChocolate.Language;

namespace HotChocolate.Stitching.Execution;

internal sealed class OperationPlanerContext
{
    public IPreparedOperation Operation { get; private set; } = default!;

    public QueryNode Plan { get; private set; } = default!;

    public QueryNode CurrentNode { get; set; } = default!;

    public Path Path { get; set; } = Path.Root;

    public NameString Source { get; set; }

    public bool NeedsInlineFragment { get; set; } = false;

    public List<IObjectType> Types { get; } = new();

    public List<ISelectionSet> SelectionSets { get; } = new();

    public List<ISyntaxNode?> Syntax { get; } = new();

    public Dictionary<string, VariableDefinitionNode> VariableDefinitions { get; } = new();

    public HashSet<string> Variables { get; } = new();

    public HashSet<string> Processed { get; } = new();

    public Dictionary<ISelectionSet, HashSet<NameString>> RequiredFields { get; } = new();

    public ObjectPool<List<ISelection>> SelectionList { get; } = new SelectionListObjectPool();

    public int Segments { get; set; }

    public void RegisterRequiredField(ISelectionSet selectionSet, NameString requiredField)
    {
        if (!RequiredFields.TryGetValue(selectionSet, out HashSet<NameString>? fields))
        {
            fields = new HashSet<NameString>();
            RequiredFields.Add(selectionSet, fields);
        }

        fields.Add(requiredField);
    }

    public void Initialize(IPreparedOperation operation, QueryNode root)
    {
        Operation = operation;
        Plan = root;
        CurrentNode = root;
        Source = root.Source;
    }

    public void Clear()
    {
        Operation = default!;
        Plan = default!;
        CurrentNode = default!;
        Path = Path.Root;
        Source = default;
        NeedsInlineFragment = false;
        Segments = default;
        Syntax.Clear();
        Types.Clear();
        SelectionSets.Clear();
        RequiredFields.Clear();
        VariableDefinitions.Clear();
        Variables.Clear();
        Processed.Clear();
    }
}
