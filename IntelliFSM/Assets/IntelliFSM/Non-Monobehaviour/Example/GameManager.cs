using IntelligentMachine.Common;
using System.Collections;


public class GameManager : ManagerBase<GameManager>
{

    /*
     * This is a Singleton that inherits from a template I created to make singletons
     * ManagerBase inherits from MonoBehaviour and allows you to attach it to game objects
     * and use the typical methods that come with that.
     * 
     * using a Singleton in this example isnt necessary, but it looks clean.
     */

    private TurnController turnController;

    private void Start()
    {
        turnController = new TurnController();
    }


    private void Update()
    {
        turnController.Update();
    }
}


