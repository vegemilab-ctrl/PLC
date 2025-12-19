using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PositionManager : MonoBehaviour
{
    [System.Serializable]
    public class Position
    {
        //포지셔닝 데이터 하나를 들고 있음.
        public int data;
        //연결되어 있는 UI
        private PositionData uiData;

        //연결된 UI 반환하는 함수
        public PositionData GetData => uiData;

        //생성자 
        public Position(int data)
        {
            this.data = data;
        }

        //UI를 연결시키는 함수
        public void ConnectUI(PositionData data)
        {
            uiData = data;
        }
    }

    //UI 원본
    public PositionData origin;
    //포지셔닝 데이터 모아두는 리스트
    public List<Position> positionList = new List<Position>();
    public ServoActuator actuator;

    //리스트에 추가하는 함수
    public void AddData(int data, bool needSave)
    {
        //포지션 데이터 생성
        Position position = new Position(data);
        //리스트에 추가.
        positionList.Add(position);
        //새로운 UI 만들고
        PositionData uiData = Instantiate(origin, transform);
        //데이터 표시
        uiData.Init(positionList.Count, data);
        uiData.gameObject.SetActive(true);
        //UI 연결
        position.ConnectUI(uiData);
        if (needSave)
            SaveData();
    }

    public void AddData()
    {
        AddData(actuator.GetCurrentPulse, true);
    }

    //리스트에서 데이터 제거하는 함수
    public void RemoveData(PositionData data)
    {
        //삭제하려는 UI와 연결된 데이터를 찾아낸다.
        Position position = positionList.Find(x => x.GetData == data);
        //데이터를 찾아내면
        if (position != null)
        {
            //리스트에서 그 데이터를 지운다.
            positionList.Remove(position);
        }

        //순서가 바뀐 리스트에 맞게 UI들의 텍스트를 변경한다.
        for (int i = 0; i < positionList.Count; ++i)
        {
            positionList[i].GetData.ChangeIndex(i);
        }
        SaveData();
    }

    //데이터 불러오기
    public void LoadData()
    {
        //이 프로그램이 실행된 폴더 경로 + PositionData.csv 경로를 만들고
        string path = Path.Combine(Application.dataPath, "PositionData.csv");
        //그 경로에 있는 파일을 불러옵니다. 불러올 때 한줄씩 라인별로 불러옵니다.
        string[] csvDatas = File.ReadAllLines(path);

        //2줄 이상의 데이터가 들어있으면 
        if (csvDatas.Length > 1)
        {
            //현재 리스트 전부 소거
            foreach (var position in positionList)
            {
                position.GetData.Delete(false);
            }
            //새로운 리스트를 만들기.
            positionList = new List<Position>();
        }

        //파일을 기준으로 다시 데이터를 생성해 리스트를 채운다.
        for (int i = 1; i < csvDatas.Length; ++i)
        {
            //','를 기준으로 문자열들을 모두 잘라내 배열에 모아둠.
            string[] datas = csvDatas[i].Split(',');
            AddData(int.Parse(datas[1]), false);
        }
    }

    //데이터 저장하기
    public void SaveData()
    {
        //이 프로그램이 실행된 폴더 경로 + PositionData.csv 경로를 만들고
        string path = Path.Combine(Application.dataPath, "PositionData.csv");

        string[] csvDatas = new string[positionList.Count + 1];
        csvDatas[0] = "id,Position";
        for (int i = 0; i < positionList.Count; ++i)
        {
            csvDatas[i + 1] = i.ToString() + ',' + positionList[i].data.ToString();
        }

        //경로에 파일 저장하기
        File.WriteAllLines(path, csvDatas);
    }


    //시작시 파일 불러오기
    private void Start()
    {
        string path = Path.Combine(Application.dataPath, "PositionData.csv");
        if (File.Exists(path))
        {
            LoadData();
        }
    }
}
