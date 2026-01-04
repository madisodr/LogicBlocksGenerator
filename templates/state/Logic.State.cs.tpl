namespace ${NAMESPACE};

using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

public partial class ${NAME}Logic
{
    [Meta]
    public abstract partial record State : StateLogic<State>;
}