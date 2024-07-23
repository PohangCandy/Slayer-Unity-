## 목차

* [개요](#개요)   
* [게임설명](#게임설명)  
* [팀원소개](#팀원소개)   
* [역할분담](#역할분담)   
* [플레이방식](#플레이방식)  
* [프로젝트 후기](#프로젝트-후기)  
* [개선점](#개선점)


---
## 개요 

-   프로젝트 이름: **DataHunter** :video_game:
-   프로젝트 지속기간: **2023.01-2023.02**
-   개발 엔진 및 언어: **Unity** / C#
-   멤버: **SweetCone**, :art: **Designer** ( 안혜지, 차규빈 ), :computer: **Programmer** ( 임건형, 민기찬)


---
## 게임설명 
**“가상세계 위협하는 바이러스, 과연 인터넷을 되찾을 수 있을까?”**
-
<a href="https://youtu.be/AtsgU5bLuS8?feature=shared" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/youtube.svg" alt="DataHunter" height="30" width="40" /></a> [사이버 펑크 미래에서 진행되는 **CCG**(Collectible Card Game) Game  ](https://youtu.be/AtsgU5bLuS8?feature=shared)
![UI_타이틀](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/94968c3f-b932-4e2e-986c-5e8548e5f8d6)


---
## 플레이방식
![_2024_06_16_00_50_42_498-ezgif com-video-to-gif-converter](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/2b7a8b09-74b2-4252-bb78-f5f25b49b318)  
보스의 방을 향해 나아가며 카드와 아이템을 수집하고 전략을 세워 적을 물리치는 게임


---
## 팀원소개 
**그래픽**
안혜지|차규빈
----|----|
![다운로드](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/7821af10-13b0-453b-af48-6266c3ed3a7e)|![다운로드](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/17788435-9f69-424e-b0cb-e0f10b1296e9)
@Instagram|@Instagram
##
**소프트웨어**
민기찬|임건형
----|----|
![화면 캡처 2024-06-10 201748](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/143a15af-e71a-40de-8554-7971a73a6d2f)|![화면 캡처 2024-06-10 200737](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/d16ede03-da73-47bc-b56f-7ad65eccaa29)|
[@github](https://github.com/PohangCandy)|[@github](https://github.com/ImGunHyoeng) |


---
## 역할분담 
### 그래픽
![header](https://capsule-render.vercel.app/api?type=waving&text=안혜지&color=FFA351FF&fontColor=FFFFFFFF&fontAlign=90&fontAlignY=40&fontSize=50)  
* 게임 기획 메인
* 캐릭터,배경 서브
##
![header](https://capsule-render.vercel.app/api?type=waving&text=차규빈&color=F99FC9FF&fontColor=FFFFFFFF&fontAlign=90&fontAlignY=40&fontSize=50)
* 캐릭터,배경 메인
* 게임 기획 서브
* UI,UX
##
### 소프트웨어
![header](https://capsule-render.vercel.app/api?type=waving&text=민기찬&color=2BAE66FF&fontColor=FCF6F5FF&fontAlign=90&fontAlignY=40&fontSize=50)

#### 1. Figma, UGUI
:page_with_curl: [UI기획서.pptx](https://github.com/user-attachments/files/15782024/UI.pptx)  
<a href="https://www.figma.com/design/QeMAo69WUSgph7Cz4x4Gmy/Slay-the-Spire-%EB%AA%A8%EC%9E%91?node-id=0-1&t=nfVWtF2NYnIzDTOg-1" target="blank"><img align="center" src="https://i.namu.wiki/i/tyR9148Wphjb2F4cAstF0NdEfTnxF5gEmmMzzjPmNzF7u7gwmk2D3USUfjJ3JA-nrvkZQAynHevRmGyrm7ciU3rdiV-rxeS2CQk_15tnzhMfVScDbzl4aMQBerHC5vZPXCT_ihMWrHh7QVBbHk3LNQ.svg" alt="Figma" height="27" width="20" /></a> [Figma 프로토타입](https://www.figma.com/design/QeMAo69WUSgph7Cz4x4Gmy/Slay-the-Spire-%EB%AA%A8%EC%9E%91?node-id=0-1&t=nfVWtF2NYnIzDTOg-1)  
![Group 21](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/10ca5570-d2eb-470e-9251-1eadbf1df62b)

* **Figma** : 피그마를 통해 기존의 CCG게임  **UI 분석** 후 프로젝트 UI 기획, **컴포넌트 기능**을 활용해 자주 쓰는 UI의 **재사용성** 높임. **ProtoType**을 수행하며 클래스 다이어그래임으로 생각해내지 못한 기획을 자세하게 수정.

* **UGUI** : 피그마에서 작성한 기획 내용을 바탕으로 구현. 배경, 오브젝트 배치 시 **Layer**를 확실하게 분리해주기 위한 카메라 설정 작업 수행

#### 2. Enemy
![Group 22](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/a12aaddc-f7e4-422f-90e9-b87b0d22adde)

* **턴 계산** : 플레이어 턴과 Enemy 턴에 따라 각 버프/디버프의 남은 **지속 시간**을 업데이트
**성능적 효율**을 위해 Update문을 쓰지 않고 턴 업데이트, 상태 전환 시 해당 지속 효과 업데이트하도록 리팩토링.

* **행동 패턴** : **FSM**을 사용해서 공격, 방어, 버프, 디버프 4가지 상태 구현. 각 상태가 끝날때마다 머리 위로 **다음 상태 예고**하도록 업데이트.

* **버프/디버프** : **Static 클래스**를 개별적으로 만들어 플레이어가 카드, 포션, 유물 등을 사용해서 Enemy 객체에 각기 다른 동작을 취할 수 있도록 만듬.


##
![header](https://capsule-render.vercel.app/api?type=waving&text=임건형&color=755139FF&fontColor=F2EDD7FF&fontAlign=90&fontAlignY=40&fontSize=50)
#### 데이터 기획&클래스
<img src="https://github.com/user-attachments/assets/b31cfd2c-cf63-41a4-8539-e8956d8b4db0" width="300" height="300">

* 전반적인 게임의 데이터들을 주고 받고하는 **설계**를 ER다이어그램을 통해서 구상

<img src="https://github.com/user-attachments/assets/a1aad14f-eb04-422c-984d-6bd877b6903f" width="300" height="300">

* 맡은 파트의 클래스에 필요한 것을 UML을 통해서 **클래스를 설계**하였다.
#### 카드 구현
<img src="https://github.com/user-attachments/assets/de3e85df-f79f-496e-a45d-fa6a7c93b853" width="300" height="300">

<img src="https://github.com/user-attachments/assets/ee9cf68f-9697-4686-b523-984287d82faa" width="300" height="300">

<img src="https://github.com/user-attachments/assets/17110e28-b55b-4e60-ae25-43c11d7f4f97" width="300" height="300">

카드를 구현할 때 **인터페이스**를 사용해서 공통된 분야를 만들고 해당하는 것으로 부터 파생되어 카드의 종류별로 클래스를 생성
FSM을 사용해서 사용자의 input에 따라서 카드의 상태를 변경하며, 상태에 맞는 행동을 하도록 구현
카드를 매니저를 통해서 관리하고 사용하여 덱에 있는 카드,뽑을 카드에 있는 목록,이미 사용한 카드등을 판단가능하도록 하였다.

#### 포션 구현

<img src="https://github.com/user-attachments/assets/b0733449-03b3-492b-9bb0-f16ddc57e3fc" width="300" height="300">

<img src="https://github.com/user-attachments/assets/17b3f9f0-357f-4582-935d-9dfca78bae49" width="300" height="300">

포션도 카드와 동일하게 인터페이스 사용,FSM 구축,매니저를 통해서 관리하였다. 
#### 플레이어 구현
캐릭터의 정보를 담아두는 저장소를 구현하였고, 상호작용할 때마다 세부내용이 변동되도록 설정.

### 추가 구현

MySQL을 php로 연동해서 구축
## 구현기능
* 사용자 등록

![사용자 등록](https://github.com/user-attachments/assets/62b9c7eb-1c02-4cea-8520-c02d894caf72)

* 로그인

![로그인](https://github.com/user-attachments/assets/337b922c-5147-4270-91af-5b508ec176c4)

  
* 로그인 시에 타이틀로 이동

  
* 로그아웃

![로그아웃](https://github.com/user-attachments/assets/5344410f-efc2-467f-b18c-4c2d316a9d43)


* 캐릭터 생성여부

![캐릭터 생성여부 확인](https://github.com/user-attachments/assets/f92c48f0-1e4e-48df-99e9-6f54d4a69486)


* 캐릭터 생성

![캐릭터 생성](https://github.com/user-attachments/assets/43d27cdb-8336-4e51-a6f0-13255c4d6265)

---
## 프로젝트 후기
### 그래픽

#### 안혜지

#### 차규빈

##
### 소프트웨어

#### 민기찬
갑작스러운 일정이었음에도 끝까지 열심히 참여해준 팀원 분들께 너무 감사하고, 지난번 프로젝트때 같이 작업 했던 분들과 함께해서 빠른 시간안에 좋은 결과물이 나온 것 같아 좋았습니다.

지난번 작품과 달리 **성능 향상**과 **최적화**에 신경을 쓰기위해서, **Update문**을 사용하지 않고 적의 **FSM**을 구현함으로써 현재 행동과 다음 행동 예고에 대한 로직을 세워보았습니다. 특히 적이 시작한 버프의 경우 다시 적의 턴이 되었을 때 1턴의 효과를 지나고, 플레이어가 건 디버프의 경우 다시 플레이어의 턴이 되었을 때 1턴이 지나는 기능을 매프레임 단위로 적의 버프, 디버프를 체크하지 않고 구현하는 작업이 상당히 까다로웠으나 턴제 게임이기에 가능한 턴 종료 버튼을 누렀을 때의 확실한 턴 전환이 있닫는 점을 생각해서 **턴 계산 방법**을 생각해낸 것이 개인적으로 뿌듯했습니다.


##
#### 임건형
급한 일정으로 인해서 몸도 힘들고 마음도 힘들었지만 혼자였다면 중간에 포기할 것 같았는데 팀원들이랑 어떻게 잘 해내서 좋았습니다.
학기중에 배웠던 내용을 활용해서 게임을 만들어내니 스킬도 늘어나고 자신감이 붙게 되어서 좋았습니다.

---
## 개선점
### 그래픽
#### 안혜지

#### 차규빈

##
### 소프트웨어

#### 민기찬 
Map을 선택했을때, 경로에 따라 갈 수 있는 다음 구간이 나눠지도록 하는 작업을 **배열**과 **상속**을 통해서 리팩토링 할 예정입니다.  
시간상 아직 구현하지 못한 **상점**과 **유물**을 구현해볼 생각입니다.


##
#### 임건형  
급하게 짜다보니 코드의 조잡한점과 구조적으로 좀 더 풀어낼수 있는 부분이 있는데 이를 집중적으로 하면서, 나중에 mysql로 카드와 포션까지 연동시켜서 작업하면은 좀 더 복잡한 부분까지 연습하고 싶습니다.




