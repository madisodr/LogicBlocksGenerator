namespace ${NAMESPACE};

using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

public interface I${NAME}Logic : ILogicBlock<${NAME}Logic.State>;

[Meta, Id("${NAME_LOWER}_logic")]
[LogicBlock(typeof(State), Diagram = true)]
public partial class ${NAME}Logic : LogicBlock<${NAME}Logic.State>, I${NAME}Logic
{
    public override Transition GetInitialState() => To<State.Default>();
}