using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Match
{
    public string matchID;

    public SyncListString bodyColors;
    public SyncListString eyesColors;

    public Match()
    {

    }

    public Match(string matchID, Color bodyColor, Color eyesColor)
    {
        this.matchID = matchID;
        bodyColors.Add(bodyColor.ToString());
        eyesColors.Add(eyesColor.ToString());
    }
}

[System.Serializable]
public class SyncListMatch : SyncList<Match> { }

public class HostAndJoin : NetworkBehaviour
{
    public Image bodyImage;
    public Image eyesImage;

    SyncListMatch matches = new SyncListMatch();
    SyncListString matchIDs = new SyncListString();

    public static string GetHostID()
    {
        string id = "";

        for (int i = 0; i < 7; i++)
        {
            int r = Random.Range(0, 36);

            if(r < 26)
            {
                id += (char)(r + 65);
            }
            else
            {
                id += (r - 26).ToString();
            }
        }

        return id;
    }

    public void HostGame()
    {
        string id = GetHostID();
        CmdHostGame(id);
    }

    [Command]
    public void CmdHostGame(string id)
    {
        if (!matchIDs.Contains(id))
        {
            matchIDs.Add(id);
            matches.Add(new Match(id, bodyImage.color, eyesImage.color));
        }
    }

    public void Join()
    {
        
    }
}
