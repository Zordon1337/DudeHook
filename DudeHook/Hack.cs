using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DudeHook
{
    class Hack:MonoBehaviour
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        Camera cam;

        GameObject Player;
        People[] PeopleVar;
        bool Tracers = false;
        bool NameESP = false;
        bool KillAll = false;
        IEnumerator CollectPlayer()
        {
            for(; ; )
            {
                Player = GameObject.FindGameObjectWithTag("Player");
                yield return new WaitForSeconds(1f);
            }
        }
        IEnumerator CollectPeople()
        {
            for(; ; )
            {
                PeopleVar = FindObjectsOfType<People>();
                yield return new WaitForSeconds(0.1f);
            }
        }
        public void Start()
        {
            StartCoroutine(CollectPeople());
            StartCoroutine(CollectPlayer());
        }
        public void OnGUI()
        {
            GUI.Label(new Rect(Screen.width/2,Screen.height/2,20,20),"+");
            
            if(GUI.Button(new Rect(0,0,125,25),"TP me"))
            {
                foreach(var p in PeopleVar)
                {
                    Player.transform.position = p.transform.position;
                }
            }

            GUI.Label(new Rect(0,25,125,25),Player.transform.position.ToString());
            if (GUI.Button(new Rect(0, 50, 125, 25), "Add 100 hp"))
            {
                FindObjectOfType<PlayerParams>().playerHealth += 100;
            }
            if (GUI.Button(new Rect(0, 75, 125, 25), "Add 1000 hp"))
            {
                FindObjectOfType<PlayerParams>().playerHealth += 1000;
            }
            if (GUI.Button(new Rect(0, 100, 125, 25), "Add 100 money"))
            {
                FindObjectOfType<PlayerParams>().GetMoney(100);
            }
            if (GUI.Button(new Rect(0, 125, 125, 25), "Add 1000 money"))
            {
                FindObjectOfType<PlayerParams>().GetMoney(1000);
            }
            if (GUI.Button(new Rect(0,150,125,25),"Kill everyone"))
            {
                foreach (var p in PeopleVar)
                {
                    p.HitMe(20000, false, false, p.gameObject);
                }
            }
            if (GUI.Button(new Rect(0, 175, 125, 25), "Snaplines: "+Tracers))
            {
                Tracers = !Tracers;
            }
            if (GUI.Button(new Rect(0, 200, 125, 25), "Name ESP: "+NameESP))
            {
                NameESP = !NameESP;
            }


            
            foreach (var p in PeopleVar)
            {
                Vector3 w2s = cam.WorldToScreenPoint(p.transform.position);


                if (Tracers)
                {
                    Render.DrawLine(new Vector2(Screen.width / 2, Screen.height / 2), new Vector2(w2s.x, Screen.height - w2s.y), Color.magenta, 5f);
                }
                if (NameESP)
                {
                    GUI.Label(new Rect((int)w2s.x - 5, Screen.height - w2s.y - 14, 40, 40), "NPC");
                }
            }

        }
        
        public void Update()
        {
            
            
        }

        public void Awake()
        {
            
            cam = Camera.main;
        }
    }
}
