using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader inst { get; private set; }

    [SerializeField] private GameObject Block;
    private float timeToLongLoad = 1;
    private static bool isLong = false;
    public void LoadScene(string scene, bool saveState = false) => StartCoroutine(FadeAndLoad(scene));
    public void ExitGame() => Application.Quit();
    private void Awake() {
        if (inst != null && inst != this) {
            Destroy(this);
        } else {
            inst = this;
        }
    }
    public void Start()
    {
        StartCoroutine(Unfade());
        AudioManager.inst.UpdateSounds();
    }
    private Vector2[] startPos = new Vector2[4]
    {
        new Vector2(1920, -1080), //лево верх
        new Vector2(-1920, -1080), //право вверх
        new Vector2(-1920, 1080), //право низ
        new Vector2(1920, 1080), //лево вниз
    };
    public SceneTransitionData[] datas = new SceneTransitionData[]
    {
        new SceneTransitionData("MainMenu","Griz"),
        new SceneTransitionData("Home","Griz"),
        new SceneTransitionData("Griz","Home"),
        new SceneTransitionData("OutsideChurch","Ending1"),
        new SceneTransitionData("Escape","Ending2"),
        new SceneTransitionData("Ending1","EndMenu"),
        new SceneTransitionData("Ending2","EndMenu")
    };
    private IEnumerator FadeAndLoad(string sceneName)
    {
        for(int i = 0; i < datas.Length; i++)
        {
            if(datas[i].Check(sceneName))
            {
                StartCoroutine(LongFade(sceneName));
                yield break;
            }
        }
        for(int i = 0; i < 2; i++)
        {
            RectTransform block = Instantiate(Block, transform).GetComponent<RectTransform>();
            block.anchoredPosition = startPos[i]* 0.75f;
            block.DOLocalMove(startPos[i] * 0.25f,0.2f);
            
            RectTransform block2 = Instantiate(Block, transform).GetComponent<RectTransform>();
            block2.anchoredPosition = startPos[i+2]* 0.75f;
            block2.DOLocalMove(startPos[i+2] * 0.25f,0.2f);
            yield return new WaitForSeconds(0.25f);
        }
        GameManager.inst.lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator Unfade()
    {     
        if(isLong)
        {
            isLong = false;
            LongUnfade();
            yield break;
        }
        RectTransform[] blocks = new RectTransform[4];
        for(int j = 0; j < 4; j++)
        {
            blocks[j] = Instantiate(Block, transform).GetComponent<RectTransform>();
            blocks[j].anchoredPosition = startPos[j]* 0.25f;
        }
        for(int i = 0; i < 2; i++)
        {
            blocks[i].DOLocalMove(startPos[i] * 0.75f,0.2f);
            blocks[i+2].DOLocalMove(startPos[i+2] * 0.75f,0.2f);
            
            yield return new WaitForSeconds(0.25f);
        }
    }
    private IEnumerator LongFade(string sceneName)
    {
        RectTransform[] blocks = new RectTransform[4];
        for(int j = 0; j < 4; j++)
        {
            blocks[j] = Instantiate(Block, transform).GetComponent<RectTransform>();
            blocks[j].anchoredPosition = startPos[j]* 0.75f;
        }
        for(int i = 0; i < 2; i++)
        {
            blocks[i].DOLocalMove(startPos[i] * 0.25f,timeToLongLoad);
            blocks[i+2].DOLocalMove(startPos[i+2] * 0.25f,timeToLongLoad);
        }
        yield return new WaitForSeconds(timeToLongLoad);
        isLong = true;
        GameManager.inst.lastSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    private void LongUnfade()
    {
        RectTransform[] blocks = new RectTransform[4];
        for(int j = 0; j < 4; j++)
        {
            blocks[j] = Instantiate(Block, transform).GetComponent<RectTransform>();
            blocks[j].anchoredPosition = startPos[j]* 0.25f;
        }
        for(int i = 0; i < 2; i++)
        {
            blocks[i].DOLocalMove(startPos[i] * 0.75f,timeToLongLoad);
            blocks[i+2].DOLocalMove(startPos[i+2] * 0.75f,timeToLongLoad);
        }
    }
}
public class SceneTransitionData
{
    public string curScene, nextScene;

    public SceneTransitionData(string c, string n)
    {
        this.curScene = c;
        this.nextScene = n;
    }
    public bool Check(string next)
    {
        return SceneManager.GetActiveScene().name == curScene && nextScene == next;
    }
}