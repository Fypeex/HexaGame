using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("SettingsScreen");
    }
    
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScreen");
    }
    
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("HomeScreen");
    }
    public void LoadShopScene()
    {
        SceneManager.LoadScene("ShopScreen");
    }
}
