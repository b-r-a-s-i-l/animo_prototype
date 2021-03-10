using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public CanvasManager Canvas { get; private set; }
    public InventoryManager Inventory { get; private set; }
    public ClockManager Clock { get; private set; }
    public GameObject Player;

    private void Awake()
    {

        Instance = this;
        Canvas = GetComponentInChildren<CanvasManager>();
        Inventory = GetComponentInChildren<InventoryManager>();
        Clock = GetComponentInChildren<ClockManager>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Sair();
        }
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }
}
