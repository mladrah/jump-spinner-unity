using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    [Header("Select Bar")]
    public Button itemShape;
    public Button itemTrail;
    public Button itemBackground;
    //public Button itemRotation;

    [Header("Item Shape Buttons")]
    public List<Button> itemShapeButtons = new List<Button>();

    [Header("Item Trail Buttons")]
    public List<Button> itemTrailButtons = new List<Button>();

    [Header("Item Background Buttons")]
    public List<Button> itemBackgroundButtons = new List<Button>();

    [Header("Shape Color Buttons")]
    public List<Button> shapeColorButtons = new List<Button>();

    [Header("Trail Color Buttons")]
    public List<Button> trailColorButtons = new List<Button>();

    [Header("Panels")]
    public GameObject itemShapePanel;
    public GameObject itemTrailPanel;
    public GameObject itemBackgroundPanel;

    [Header("Misc")]
    public GameObject gameManager;
    public GameObject camera;
    public GameObject currencyManager;
    public GameObject trailManager;
    public GameObject obstacleSpawner;
    public GameObject buyPanel;
    public GameObject okayPanel;
    public GameObject currentPlayer;
    public Animator boughtItem;

    //BEIM ERWEITERN DIE GRÖSSE ÄNDERN IM INSPEKTOR
    public GameObject[] itemShapeClosedList;
    public GameObject[] itemShapePriceList;

    public GameObject[] itemTrailClosedList;
    public GameObject[] itemTrailPriceList;

    public GameObject[] itemBackgroundClosedList;
    public GameObject[] itemBackgroundPriceList;

    [Header("Player List")]
    public GameObject playerList;

    private bool[] boughtItemShapes;
    private bool[] boughtItemTrails;
    private bool[] boughtItemBackgrounds;

    private int currentSelectedIndex;
    private string currentSelectedItem;

    private Color selectedShapeColor;
    private int selectedShapeColorIndex;
    private Color selectedTrailColor;
    private int selctedTrailColorIndex;

    private int initializeOnce;
    public bool reset = false;
    public bool test = false;
    public string testPrice;

    private int selectedImageIndex = 2; //Selected Image Position from Item Button which is already bought
    private int selectedBuyIndex = 3; //Selected Image Position from Item Button which is not bought
    private int selectedColorIndex = 0; //Selected Image Position from Color Buttons
    private int selectedSelectBar = 1;

    private void Start()
    {
        Initialize();

        CheckForBoughtItems();
        CheckForPlayerAppearance();

    }

    private void Initialize() {

        initializeOnce = PlayerPrefs.GetInt("Initialized");
        if (initializeOnce != 5 || reset) {

            boughtItemShapes = new bool[itemShapeButtons.Count];
            boughtItemShapes[0] = true;

            boughtItemTrails = new bool[itemTrailButtons.Count];
            boughtItemTrails[0] = true;

            boughtItemBackgrounds = new bool[itemBackgroundButtons.Count];
            boughtItemBackgrounds[0] = true;

            PlayerPrefsX.SetBoolArray("Bought Item Shapes", boughtItemShapes);
            PlayerPrefsX.SetBoolArray("Bought Item Trails", boughtItemTrails);
            PlayerPrefsX.SetBoolArray("Bought Item Backgrounds", boughtItemBackgrounds);

            PlayerPrefs.SetInt("Player Shape", 0);
            PlayerPrefs.SetInt("Shape Color", 0);
            PlayerPrefs.SetInt("Trail", 0);
            PlayerPrefs.SetInt("Trail Color", 0);
            PlayerPrefs.SetInt("Background", 0);

            PlayerPrefs.SetInt("Initialized", 5);
            Debug.Log("Initialized");
        }

        if (test) {
            for (int i = 0; i < itemShapePriceList.Length; i++)
                itemShapePriceList[i].GetComponent<TextMeshProUGUI>().text = testPrice;
            for (int i = 0; i < itemTrailPriceList.Length; i++)
                itemTrailPriceList[i].GetComponent<TextMeshProUGUI>().text = testPrice;
            for (int i = 0; i < itemBackgroundPriceList.Length; i++)
                itemBackgroundPriceList[i].GetComponent<TextMeshProUGUI>().text = testPrice;
        }

    }

    private void CheckForBoughtItems() {

        boughtItemShapes = PlayerPrefsX.GetBoolArray("Bought Item Shapes");
        boughtItemTrails = PlayerPrefsX.GetBoolArray("Bought Item Trails");
        boughtItemBackgrounds = PlayerPrefsX.GetBoolArray("Bought Item Backgrounds");

        for (int i = 0; i < boughtItemShapes.Length; i++) { 
            if (boughtItemShapes[i])
                itemShapeClosedList[i].SetActive(false);         
        }

        for(int i=0; i<boughtItemTrails.Length; i++) {
            if (boughtItemTrails[i])
                itemTrailClosedList[i].SetActive(false);
        }

        for(int i=0; i<boughtItemBackgrounds.Length; i++) {
            if (boughtItemBackgrounds[i])
                itemBackgroundClosedList[i].SetActive(false);
        }

    }

    private void CheckForPlayerAppearance() {
        //SHAPE
        currentPlayer.SetActive(false);
        currentPlayer = playerList.GetComponent<PlayerList>().playerList[PlayerPrefs.GetInt("Player Shape")];
        currentPlayer.SetActive(true);
        ReplacePlayer();

        //SHAPE COLOR
        selectedShapeColor = shapeColorButtons[PlayerPrefs.GetInt("Shape Color")].GetComponent<Image>().color;
        ReplaceShapeColor();

        //SELECT
        SelectCurrentButton(itemShapeButtons, PlayerPrefs.GetInt("Player Shape"));
        SelectCurrentColorButton(shapeColorButtons, PlayerPrefs.GetInt("Shape Color"));

        //TRAILS
        for (int i = 0; i < currentPlayer.transform.childCount; i++) {
            trailManager.GetComponent<TrailManager>().ChangeTrail(currentPlayer.transform.GetChild(i).gameObject, PlayerPrefs.GetInt("Trail"));
        }

        //TRAIL COLOR
        selectedTrailColor = trailColorButtons[PlayerPrefs.GetInt("Trail Color")].GetComponent<Image>().color;
        ReplaceTrailColor();

        //BACKGROUND
        SelectCurrentButton(itemBackgroundButtons, PlayerPrefs.GetInt("Background"));
        BackgroundColor(PlayerPrefs.GetInt("Background"));
        
        //SELECT
        SelectCurrentButton(itemTrailButtons, PlayerPrefs.GetInt("Trail"));
        SelectCurrentColorButton(trailColorButtons, PlayerPrefs.GetInt("Trail Color"));

        //SELECTBAR
        SelectCurrentSelectBarButton(itemShape);

    }

    public void OnClickShape(int index) 
    {    
        currentSelectedItem = "Shape";
        currentSelectedIndex = index;

        if (!boughtItemShapes[index]) {
            OpenBuyPanel(index);
            return;
        }

        DeactivateBuyPanel();

        DeselectFormerButtons(itemShapeButtons);

        currentPlayer.SetActive(false);
        currentPlayer = playerList.GetComponent<PlayerList>().playerList[index];
        currentPlayer.SetActive(true);

        SelectCurrentButton(itemShapeButtons, index);

        ReplacePlayer();
        ReplaceShapeColor();
        ReplaceTrail();
        ReplaceTrailColor();

        PlayerPrefs.SetInt("Player Shape", index);

    }

    public void OnClickTrail(int index)
    {
        currentSelectedItem = "Trail";
        currentSelectedIndex = index;

        if (!boughtItemTrails[index]) {
            OpenBuyPanel(index);
            return;
        }

        DeactivateBuyPanel();

        DeselectFormerButtons(itemTrailButtons);

        for (int i=0; i<currentPlayer.transform.childCount; i++) {

            trailManager.GetComponent<TrailManager>().ChangeTrail(currentPlayer.transform.GetChild(i).gameObject, index);

        }

        SelectCurrentButton(itemTrailButtons, index);

        PlayerPrefs.SetInt("Trail", index);
    }

    public void OnClickBackground(int index) {
        currentSelectedItem = "Background";
        currentSelectedIndex = index;

        if (!boughtItemBackgrounds[index]) {
            OpenBuyPanel(index);
            return;
        }

        DeactivateBuyPanel();

        DeselectFormerButtons(itemBackgroundButtons);

        SelectCurrentButton(itemBackgroundButtons, index);

        if (PlayerPrefs.GetInt("Background") != index)
            okayPanel.SetActive(true);
        else
            okayPanel.SetActive(false);

    }

    private void OpenBuyPanel(int index) {
        okayPanel.SetActive(false);
        if (currentSelectedItem.Equals("Shape")) { 
            DeselectFormerBuyButton(itemShapeButtons);
            SelectCurrentBuyButton(itemShapeButtons, index);
        } else if (currentSelectedItem.Equals("Trail")) {
            DeselectFormerBuyButton(itemTrailButtons);
            SelectCurrentBuyButton(itemTrailButtons, index);
        } else if (currentSelectedItem.Equals("Background")) {
            DeselectFormerBuyButton(itemBackgroundButtons);
            SelectCurrentBuyButton(itemBackgroundButtons, index);
        }

        buyPanel.SetActive(true);
    }

    public void Buy() { 
        if (currentSelectedItem.Equals("Shape")) {

            if (BuyProgress(itemShapePriceList, itemShapeClosedList, boughtItemShapes, "Bought Item Shapes"))
                OnClickShape(currentSelectedIndex);
                
        } else if(currentSelectedItem.Equals("Trail")) {

            if (BuyProgress(itemTrailPriceList, itemTrailClosedList, boughtItemTrails, "Bought Item Trails"))
                OnClickTrail(currentSelectedIndex);

        } else if (currentSelectedItem.Equals("Background")) {

            if (BuyProgress(itemBackgroundPriceList, itemBackgroundClosedList, boughtItemBackgrounds, "Bought Item Backgrounds"))
                OnClickBackground(currentSelectedIndex);
        }

    }

    public void ConfirmBackground() {
        BackgroundColor(currentSelectedIndex);

        PlayerPrefs.SetInt("Background", currentSelectedIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void BackgroundColor(int index) {
        ColorManagerObstacle.SetAllFalse();
        if (index == 0)
            ColorManagerObstacle.defaultColor = true;
        else if (index == 1)
            ColorManagerObstacle.blackWhiteColor = true;
        else if (index == 2)
            ColorManagerObstacle.redColor = true;
        else if (index == 3)
            ColorManagerObstacle.greenColor = true;
        else if (index == 4)
            ColorManagerObstacle.blueColor = true;
        else if (index == 5)
            ColorManagerObstacle.yellowColor = true;
        else if (index == 6)
            ColorManagerObstacle.turkeyColor = true;
        else if (index == 7)
            ColorManagerObstacle.pinkColor = true;
        else if (index == 8)
            ColorManagerObstacle.orangeColor = true;
    }

    public void OnClickShapeColor(int index) {
        DeselectFormerColorButton(shapeColorButtons);
        SelectCurrentColorButton(shapeColorButtons, index);

        selectedShapeColor = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        ReplaceShapeColor();
        PlayerPrefs.SetInt("Shape Color", index);
    }

    public void OnClickTrailColor(int index) {
        DeselectFormerColorButton(trailColorButtons);
        SelectCurrentColorButton(trailColorButtons, index);

        PlayerPrefs.SetInt("Trail Color", index);
        if (index == 0) {
            ReplaceTrailColor();
            return;
        }
        selectedTrailColor = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
        ReplaceTrailColor();
    }

    private bool BuyProgress(GameObject[] priceList, GameObject[] closedList, bool[] boughtList, string boughtListName) {

        int price = int.Parse(priceList[currentSelectedIndex].GetComponent<TextMeshProUGUI>().text);
        int money = currencyManager.GetComponent<CurrencyManager>().currencyAmount;

        if (money >= price) {
            closedList[currentSelectedIndex].SetActive(false);

            boughtList[currentSelectedIndex] = true;
            PlayerPrefsX.SetBoolArray(boughtListName, boughtList);

            DeactivateBuyPanel();

            currencyManager.GetComponent<CurrencyManager>().ReduceCurrency(price);
            boughtItem.SetTrigger("bought_item");
            PlayerPrefs.SetInt("Currency", currencyManager.GetComponent<CurrencyManager>().currencyAmount);
            return true;
        } else
            Debug.Log("Not Enough Money");
        return false;
    }

    public void OpenItemShape() {
        DeactivateBuyPanel();
        DeactivateOkayPanel();

        DeselectFormerSelectBarButton();
        SelectCurrentSelectBarButton(itemShape);

        itemTrailPanel.SetActive(false);
        itemBackgroundPanel.SetActive(false);
        itemShapePanel.SetActive(true);
    }

    public void OpenItemTrail() {
        DeactivateBuyPanel();
        DeactivateOkayPanel();

        DeselectFormerSelectBarButton();
        SelectCurrentSelectBarButton(itemTrail);

        itemShapePanel.SetActive(false);
        itemBackgroundPanel.SetActive(false);
        itemTrailPanel.SetActive(true);
    }

    public void OpenItemBackground() {
        DeactivateBuyPanel();

        DeselectFormerSelectBarButton();
        SelectCurrentSelectBarButton(itemBackground);

        itemShapePanel.SetActive(false);
        itemTrailPanel.SetActive(false);
        itemBackgroundPanel.SetActive(true);
    }

    private void ReplacePlayer() {
        camera.GetComponent<CameraMovement>().target = currentPlayer.transform;
        gameManager.GetComponent<GameManager>().player = currentPlayer;
        currencyManager.GetComponent<CurrencyManager>().player = currentPlayer;
    }

    private void ReplaceTrail() {

        for (int i = 0; i < currentPlayer.transform.childCount; i++) {
            trailManager.GetComponent<TrailManager>().ChangeTrail(currentPlayer.transform.GetChild(i).gameObject, PlayerPrefs.GetInt("Trail"));
        }
    }

    private void ReplaceShapeColor() {
        currentPlayer.GetComponent<SpriteRenderer>().color = selectedShapeColor;

        //Change Death Particle Color
        ParticleSystem ps = currentPlayer.GetComponent<Player>().deathParticlePrefab.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule ma = ps.main;
        ma.startColor = selectedShapeColor;

        for (int i=0; i<currentPlayer.transform.childCount; i++) {
            trailManager.GetComponent<TrailManager>().ChangeTrailStartColor(currentPlayer.transform.GetChild(i).gameObject, selectedShapeColor);
        }
    }

    private void ReplaceTrailColor() {
        for (int i = 0; i < currentPlayer.transform.childCount; i++) {

            if(PlayerPrefs.GetInt("Trail Color")==0)
                trailManager.GetComponent<TrailManager>().ChangeTrailColorRandom(currentPlayer.transform.GetChild(i).gameObject);
            else
                trailManager.GetComponent<TrailManager>().ChangeTrailEndColor(currentPlayer.transform.GetChild(i).gameObject, selectedTrailColor);
        }
    }

    private void DeselectFormerButtons(List<Button> btns) {
        for (int i = 0; i < btns.Count; i++) {
            if (btns[i].transform.GetChild(selectedImageIndex).gameObject.activeInHierarchy)
                btns[i].transform.GetChild(selectedImageIndex).gameObject.SetActive(false);
        }
    }

    private void SelectCurrentButton(List<Button> btns, int index) {
        btns[index].transform.GetChild(selectedImageIndex).gameObject.SetActive(true);
    }

    private void DeselectFormerBuyButton(List<Button> btns) {
        for (int i = 0; i < btns.Count; i++) {
            if (btns[i].transform.GetChild(selectedBuyIndex).gameObject.activeInHierarchy)
                btns[i].transform.GetChild(selectedBuyIndex).gameObject.SetActive(false);
        }
    }

    private void SelectCurrentBuyButton(List<Button> btns, int index) {
        btns[index].transform.GetChild(selectedBuyIndex).gameObject.SetActive(true);

    }
    private void DeselectFormerColorButton(List<Button> btns) {
        for (int i = 0; i < btns.Count; i++) {
            if (btns[i].transform.GetChild(selectedColorIndex).gameObject.activeInHierarchy)
                btns[i].transform.GetChild(selectedColorIndex).gameObject.SetActive(false);
        }
    }
    
    private void SelectCurrentColorButton(List<Button> btns, int index) {
        btns[index].transform.GetChild(selectedColorIndex).gameObject.SetActive(true);
    }


    private void DeselectFormerSelectBarButton() {
        itemShape.transform.GetChild(selectedSelectBar).gameObject.SetActive(false);
        itemTrail.transform.GetChild(selectedSelectBar).gameObject.SetActive(false);
        itemBackground.transform.GetChild(selectedSelectBar).gameObject.SetActive(false);
    }

    private void SelectCurrentSelectBarButton(Button btn) {
        btn.transform.GetChild(selectedSelectBar).gameObject.SetActive(true);
    }

    private void DeactivateBuyPanel() {
        buyPanel.SetActive(false);
        DeselectFormerBuyButton(itemShapeButtons);
        DeselectFormerBuyButton(itemTrailButtons);
        DeselectFormerBuyButton(itemBackgroundButtons);
    }

    private void DeactivateOkayPanel() {
        DeselectFormerButtons(itemBackgroundButtons);
        SelectCurrentButton(itemBackgroundButtons, PlayerPrefs.GetInt("Background"));
        okayPanel.SetActive(false);
    }

    public void Back() {
        DeactivateBuyPanel();
        DeactivateOkayPanel();
    }
}
