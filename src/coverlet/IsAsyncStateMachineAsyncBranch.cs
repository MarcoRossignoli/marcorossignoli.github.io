        private bool IsAsyncStateMachineAsyncBranch(TypeDefinition typeDef, MethodDefinition method, Instruction currentInstruction, ILProcessor processor)
        {
            if (!method.FullName.EndsWith("::MoveNext()"))
            {
                return false;
            }

            List<(Instruction start, Instruction end)> branchToSkip = new List<(Instruction start, Instruction end)>();

            foreach (InterfaceImplementation implementedInterface in typeDef.Interfaces)
            {
                if (implementedInterface.InterfaceType.FullName == "System.Runtime.CompilerServices.IAsyncStateMachine")
                {
                    Instruction startAsyncBranch = null;
                    Instruction leaveAsyncBranch = null;
                    foreach (var bodyInstruction in processor.Body.Instructions)
                    {
                        if (startAsyncBranch is null &&
                            bodyInstruction.Operand is MethodReference operand &&
                            operand.Name == "get_IsCompleted" &&
                            operand.DeclaringType.FullName.StartsWith("System.Runtime.CompilerServices.TaskAwaiter") &&
                            operand.DeclaringType.Scope.Name == "System.Runtime")
                        {
                            startAsyncBranch = bodyInstruction;
                        }
                        if (!(startAsyncBranch is null) && leaveAsyncBranch is null && bodyInstruction.OpCode == OpCodes.Leave)
                        {
                            leaveAsyncBranch = bodyInstruction;
                            branchToSkip.Add((startAsyncBranch, leaveAsyncBranch));
                            startAsyncBranch = null;
                            leaveAsyncBranch = null;
                        }
                    }
                }
            }

            foreach ((Instruction start, Instruction end) in branchToSkip)
            {
                if (currentInstruction.Offset >= start.Offset && currentInstruction.Offset <= end.Offset)
                {
                    return true;
                }
            }

            return false;
        }
