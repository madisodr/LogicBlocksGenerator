namespace ${NAMESPACE};

using Chickensoft.Introspection;
using Chickensoft.LogicBlocks;

using Godot;

public partial class ${NAME}Logic
{
    public partial record State
    {
        [Meta, Id("${NAME_LOWER}_logic_state_default")]
        public abstract partial record Default : State,
        IGet<Input.Default>
        {
            public Default()
            {
                this.OnEnter(() =>
                {
                    // EDIT ME
                });

                this.OnExit(() =>
                {
                    // EDIT ME
                });
            }

            public Transition On(in Input.Default input)
            {
                // EDIT ME
                return ToSelf();
            }
        }
    }

}