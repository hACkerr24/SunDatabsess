using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClientListUI : MonoBehaviour
{
    public GameObject clientItemPrefab;
    public Transform clientListContainer;
    public Dropdown filterDropdown;
    public GameObject popupPanel;
    public Text popupNameText;
    public Text popupPointsText;
    public Text popupAddressText;

    private void Start()
    {
        APIService.APIDataReceived += HandleAPIDataReceived;
        GetComponent<APIService>().FetchAPIData();
    }

    private void HandleAPIDataReceived(string jsonData)
    {
        // Parse JSON data into an array of ClientData objects
        ClientData[] clientDataArray = JsonUtility.FromJson<ClientData[]>(jsonData);

        // Instantiate and populate client items based on the array
        foreach (ClientData clientData in clientDataArray)
        {
            GameObject clientItem = Instantiate(clientItemPrefab, clientListContainer);
            clientItem.GetComponentInChildren<Text>().text = clientData.label + " - Points: " + clientData.points;

            // Add click event to open the popup
            clientItem.GetComponent<Button>().onClick.AddListener(() => ShowPopup(clientData));
        }
    }

    private void ShowPopup(ClientData clientData)
    {
        // Populate popup with client data
        popupNameText.text = clientData.label;
        popupPointsText.text = "Points: " + clientData.points.ToString();
        popupAddressText.text = "Address: " + clientData.address;

        // Show the popup with animation
        popupPanel.SetActive(true);
        popupPanel.transform.localScale = Vector3.zero;
        popupPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void ClosePopup()
    {
        // Close the popup with animation
        popupPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() => popupPanel.SetActive(false));
    }
}
