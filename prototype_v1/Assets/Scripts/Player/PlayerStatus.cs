using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Bars { Sanity, Hunger, Thirst, Fatigue, Toilet }

public class PlayerStatus : MonoBehaviour
{

    [Header("Bars")]
    public GameObject[] bars;

    private float pointsSanity;
    private float pointsHunger;
    private float pointsThirst;
    private float pointsFatigue;
    private float pointsToilet;

    private ClockManager clock;

    //methods get-set
    public float PointsSanity { get => pointsSanity; set => pointsSanity = value; }
    public float PointsHunger { get => pointsHunger; set => pointsHunger = value; }
    public float PointsThirst { get => pointsThirst; set => pointsThirst = value; }
    public float PointsFatigue { get => pointsFatigue; set => pointsFatigue = value; }
    public float PointsToilet { get => pointsToilet; set => pointsToilet = value; }

    private void Start()
    {
        pointsSanity = 100;
        pointsHunger = 100;
        pointsThirst = 100;
        pointsFatigue = 100;
        pointsToilet = 100;

        clock = GameManager.Instance.Clock;
    }

    private void Update()
    {
        Debugs();
        UpdateValues();
        UpdateTransforms();
        TimeInfluence();
    }

    private void UpdateValues()
    {
        Vector3 vector;
        float points;

        points = pointsSanity / 100f;
        vector = new Vector3(points, 1, 1);
        bars[0].GetComponent<RectTransform>().localScale = vector;

        points = pointsHunger / 100f;
        vector = new Vector3(points, 1, 1);
        bars[1].GetComponent<RectTransform>().localScale = vector;

        points = pointsThirst / 100f;
        vector = new Vector3(points, 1, 1);
        bars[2].GetComponent<RectTransform>().localScale = vector;

        points = pointsFatigue / 100f;
        vector = new Vector3(points, 1, 1);
        bars[3].GetComponent<RectTransform>().localScale = vector;

        points = pointsToilet / 100f;
        vector = new Vector3(points, 1, 1);
        bars[4].GetComponent<RectTransform>().localScale = vector;
    }

    private void UpdateTransforms()
    {
        foreach (GameObject bar in bars)
        {
            Image img = bar.GetComponent<Image>();
            RectTransform rT = bar.GetComponent<RectTransform>();
            float barScale = rT.localScale.x;

            CheckLimits(barScale, rT);
            UpdateColors(barScale, img);
        }
    }

    private void CheckLimits(float barScale, RectTransform rT)
    {
        // min-max values
        if (barScale > 1f)
        {
            rT.localScale = new Vector3(1f, rT.localScale.y);
        }
        if (barScale < 0f)
        {
            rT.localScale = new Vector3(0f, rT.localScale.y);
        }
    }

    private void UpdateColors(float barScale, Image img)
    {
        //bar colour
        if (barScale > .1f)
        {
            img.color = new Color(100f, 0f, 0f, 255f);
        }
        if (barScale > .3f)
        {
            img.color = new Vector4(255f, 255f, 0f, 255f);
        }
        if (barScale > .7f)
        {
            img.color = new Color(0f, 50f, 0f, 255f);
        }
    }

    private void TimeInfluence()
    {
        //time influence in the status player with random value
        if (clock.Seconds > 9.95)
        {

            float random = Random.Range(0, 1.5f);

            if (clock.Minutes == 0)
            {
                EditPoints(Bars.Thirst, random);
            }
            if (clock.Minutes == 20)
            {
                EditPoints(Bars.Hunger, random);
            }
            if (clock.Minutes == 40)
            {
                EditPoints(Bars.Toilet, random);
            }
            if (clock.Minutes == 50)
            {
                EditPoints(Bars.Fatigue, random);
            }
        }    
    }

    public void EditPoints(Bars name, float value, bool add = false)
    {
        switch (name)
        {
            case Bars.Sanity:
                if (add) pointsSanity += value;
                else pointsSanity -= value;
                break;

            case Bars.Hunger:
                if (add) pointsHunger += value;
                else pointsHunger -= value;
                break;

            case Bars.Thirst:
                if (add) pointsThirst += value;
                else pointsThirst -= value;
                break;

            case Bars.Fatigue:
                if (add) pointsFatigue += value;
                else pointsFatigue -= value;
                break;

            case Bars.Toilet:
                if (add) pointsToilet += value;
                else pointsToilet -= value;
                break;
        }
    }

    private void Debugs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) EditPoints(Bars.Sanity, 10);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EditPoints(Bars.Hunger, 10);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EditPoints(Bars.Thirst, 10);
        if (Input.GetKeyDown(KeyCode.Alpha4)) EditPoints(Bars.Fatigue, 10);
        if (Input.GetKeyDown(KeyCode.Alpha5)) EditPoints(Bars.Toilet, 10);

        if (Input.GetKeyDown(KeyCode.Alpha6)) EditPoints(Bars.Sanity, 10);
        if (Input.GetKeyDown(KeyCode.Alpha7)) EditPoints(Bars.Hunger, 10);
        if (Input.GetKeyDown(KeyCode.Alpha8)) EditPoints(Bars.Thirst, 10);
        if (Input.GetKeyDown(KeyCode.Alpha9)) EditPoints(Bars.Fatigue, 10);
        if (Input.GetKeyDown(KeyCode.Alpha0)) EditPoints(Bars.Toilet, 10);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("SAN: " + pointsSanity);
            Debug.Log("HUN: " + pointsHunger);
            Debug.Log("THI: " + pointsThirst);
            Debug.Log("FAT: " + pointsFatigue);
            Debug.Log("TOI: " + pointsToilet);
        }
    }
}