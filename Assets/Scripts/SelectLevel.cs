using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Text LevelName;
    public Button PlayButton;
    public Camera Level01Camera;
    public Camera Level02Camera;
    public Camera LevelBossCamera;
    public Camera MainCamera;
    private string _activeLevelInfo;
    private Ray _ray;
    private RaycastHit _hit;
    private bool _showingInfo;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        _ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(MainCamera.transform.position, _ray.direction, out _hit, 100f))
            {
                print(_hit.transform.name );
                if(_hit.transform.name == "Level02Island")
                    Level02();

            }

        }
    }

    public void Back_Button()
    {
        if(!_showingInfo)
         SceneManager.LoadScene("Scenes/MainMenu");
        else
        {
            SwitchUI(false, "");
            switch (_activeLevelInfo)
            {
                case "Level01":
                    Level01Camera.gameObject.SetActive(false);
                    break;
                case "Level02":
                    Level02Camera.gameObject.SetActive(false);
                break;
                case "LevelBoss":
                    LevelBossCamera.gameObject.SetActive(false);
                    break;
            }
            

        }
    }

    public void Play_Button()
    {
        print(_activeLevelInfo);
        SceneManager.LoadScene(_activeLevelInfo);
    }

    private void Level01()
    {
        SwitchUI(true, "Level 1");
        _activeLevelInfo = "Level01";
        Level02Camera.gameObject.SetActive(true);
    }
    private void Level02()
    {
        SwitchUI(true, "Level 2");
        _activeLevelInfo = "Level02";
        Level02Camera.gameObject.SetActive(true);
    }
    
    private void LevelBoss()
    {
        SwitchUI(true, "Boss Level");
        _activeLevelInfo = "LevelBoss";
        Level02Camera.gameObject.SetActive(true);
    }

    private void SwitchUI(bool value, string level)
    {
        MainCamera.gameObject.SetActive(!value);
        _showingInfo = value;
        LevelName.gameObject.SetActive(value);
        if(level != "")
            LevelName.text = level;
        else
            LevelName.gameObject.SetActive(false);
        PlayButton.gameObject.SetActive(value);
    }
}