[System.Serializable]
public class PlayerData
{
    private int currentLevel;
    
    public PlayerData(int level)
    {
        currentLevel = level;
    }
    public int getLevel()
    {
        return currentLevel;
    }
}
