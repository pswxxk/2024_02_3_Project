using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand 
{ 
    void Execute();
    void Undo();
}

public class MoveCommand : ICommand
{ 
    private Transform objectToMove;
    private Vector3 displacement;

    public MoveCommand(Transform obj, Vector3 displacement)
    {
        this.objectToMove = obj;
        this.displacement = displacement;
    }

    public void Execute() { objectToMove.position += displacement; }
    
    public void Undo() { objectToMove.position -= displacement; }
}



public class CommandManager : MonoBehaviour
{
    private Stack<ICommand> commandHistory = new Stack<ICommand>();             //Stack 자료구조 형태로 커맨드 관리
    
    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        commandHistory.Push(command);                                           //Stack에 넣는다
    }

    public void UndoLastCommand()
    {
        if (commandHistory.Count > 0)
        {
            ICommand lastCommand = commandHistory.Pop();                        //Stack에서 빼낸다.
            lastCommand.Undo();
        }
    }
}
