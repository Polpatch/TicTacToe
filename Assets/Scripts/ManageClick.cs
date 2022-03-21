using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ManageClick : MonoBehaviour
{
    /// <summary>
    /// SpriteRendered component of gameObject, used to set the correct sprite on board
    /// </summary>
    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Contains Sprite for player X and Player O
    /// </summary>
    /// <typeparam name="string">name of the palyer</typeparam>
    /// <typeparam name="Sprite">associated sprite</typeparam>
    /// <returns></returns>
    public SortedDictionary<string, Sprite> spriteMap = new SortedDictionary<string, Sprite>();

    public Vector2Int grindPosition;

    private CoreGame coreGameIstance;

    public static UnityEvent resetEvent;

    void Start()
    {
        this.coreGameIstance = CoreGame.Instance;
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // Debug.Log("calling loadsprite O");
        StartCoroutine(this.loadSpritesWhenReady("O"));
        // Debug.Log("calling loadsprite X");
        StartCoroutine(this.loadSpritesWhenReady("X"));


        if (ManageClick.resetEvent == null)
            ManageClick.resetEvent = new UnityEvent();
        
        ManageClick.resetEvent.AddListener(ResetSprite);
    }

    /// <summary>
    /// Manages the loading of sprites for X and O players
    /// </summary>
    /// <param name="fileName">name of the file</param>
    /// <returns>IEnumarator</returns>
    IEnumerator loadSpritesWhenReady(string fileName){
        // Debug.Log("Loading sprites");
        AsyncOperationHandle<Sprite> spriteHandle = Addressables.LoadAssetAsync<Sprite>("Assets/Textures/"+fileName+".png");
        yield return spriteHandle;
        if(spriteHandle.Status == AsyncOperationStatus.Succeeded){
            this.spriteMap.Add(fileName, spriteHandle.Result);
        }
    }

    void OnMouseUp(){

        if(Input.GetMouseButtonUp(0)){
            if(this.spriteRenderer.sprite == null) {
                changeSprite(getCurrentPlayer());
                this.coreGameIstance.cellIsPressed(this.grindPosition);
            }
        }

    }

    /// <summary>
    /// Set the sprite on gameObject according to the current player
    /// </summary>
    /// <param name="currentPlayer">name of the player currently active in the round</param>
    void changeSprite(string currentPlayer){
        Sprite sprite = null;

        this.spriteMap.TryGetValue(currentPlayer, out sprite);

        this.spriteRenderer.sprite = sprite;
    }

    /// <summary>
    /// Call CoreGame singleton to get the current player
    /// </summary>
    /// <returns>Name of the player</returns>
    string getCurrentPlayer(){
        return this.coreGameIstance.getCurrentSymbol();
    }

    private void ResetSprite(){
        this.spriteRenderer.sprite = null;
    }
}
