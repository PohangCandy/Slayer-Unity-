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

![header](https://capsule-render.vercel.app/api?type=waving&text=차규빈&color=F99FC9FF&fontColor=FFFFFFFF&fontAlign=90&fontAlignY=40&fontSize=50)
* 캐릭터,배경 메인
* 게임 기획 서브
* UI,UX
##
### 소프트웨어
![header](https://capsule-render.vercel.app/api?type=waving&text=민기찬&color=2BAE66FF&fontColor=FCF6F5FF&fontAlign=90&fontAlignY=40&fontSize=50)

#### 1. Figma, UGUI
:page_with_curl:[UI기획서.pptx](https://github.com/user-attachments/files/15782024/UI.pptx)  
[Figma 프로토타입](https://www.figma.com/design/QeMAo69WUSgph7Cz4x4Gmy/Slay-the-Spire-%EB%AA%A8%EC%9E%91?node-id=0-1&t=nfVWtF2NYnIzDTOg-1)  
![_2024_06_10_22_02_08_820-ezgif com-video-to-gif-converter (1)](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/6118f714-8bc2-4b98-b4d4-1df8df6c0e05)  
![Group 21](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/10ca5570-d2eb-470e-9251-1eadbf1df62b)

* **Figma** : 피그마를 통해 기존의 CCG게임  **UI 분석** 후 프로젝트 UI 기획, **컴포넌트 기능**을 활용해 자주 쓰는 UI의 **재사용성** 높임. **ProtoType**을 수행하며 클래스 다이어그래임으로 생각해내지 못한 기획을 자세하게 수정.

* **UGUI** : 피그마에서 작성한 기획 내용을 바탕으로 구현. 배경, 오브젝트 배치 시 **Layer**를 확실하게 분리해주기 위한 카메라 설정 작업 수행

#### 2. Enemy
![Group 22](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/a12aaddc-f7e4-422f-90e9-b87b0d22adde)

* **턴 계산** : 플레이어 턴과 Enemy 턴에 따라 각 버프/디버프의 남은 **지속 시간**을 업데이트
**성능적 효율**을 위해 Update문을 쓰지 않고 턴 업데이트, 상태 전환 시 해당 지속 효과 업데이트하도록 리팩토링.

* **행동 패턴** : **FSM**을 사용해서 공격, 방어, 버프, 디버프 4가지 상태 구현. 각 상태가 끝날때마다 머리 위로 **다음 상태 예고**하도록 업데이트.

* **버프/디버프** : **Static 클래스**를 개별적으로 만들어 플레이어가 카드, 포션, 유물 등을 사용해서 Enemy 객체에 각기 다른 동작을 취할 수 있도록 만듬.



![header](https://capsule-render.vercel.app/api?type=waving&text=임건형&color=755139FF&fontColor=F2EDD7FF&fontAlign=90&fontAlignY=40&fontSize=50)
#### 데이터 기획

#### 카드 구현

#### 포션 구현

#### 플레이어 구현


---
## 프로젝트 후기
### 그래픽
#### 안혜지

#### 차규빈


### 소프트웨어
#### 민기찬
갑작스러운 일정이었음에도 끝까지 열심히 참여해준 팀원 분들께 너무 감사하고, 지난번 프로젝트때 같이 작업 했던 분들과 함께해서 빠른 시간안에 좋은 결과물이 나온 것 같아 좋았습니다.

지난번 작품과 달리 **성능 향상**과 **최적화**에 신경을 쓰기위해서, **Update문**을 사용하지 않고 적의 **FSM**을 구현함으로써 현재 행동과 다음 행동 예고에 대한 로직을 세워보았습니다. 특히 적이 시작한 버프의 경우 다시 적의 턴이 되었을 때 1턴의 효과를 지나고, 플레이어가 건 디버프의 경우 다시 플레이어의 턴이 되었을 때 1턴이 지나는 기능을 매프레임 단위로 적의 버프, 디버프를 체크하지 않고 구현하는 작업이 상당히 까다로웠으나 턴제 게임이기에 가능한 턴 종료 버튼을 누렀을 때의 확실한 턴 전환이 있닫는 점을 생각해서 **턴 계산 방법**을 생각해낸 것이 개인적으로 뿌듯했습니다.

#### 임건형


---
## 개선점
### 그래픽
**안혜지**

**차규빈**


### 소프트웨어
**민기찬**
프로그래밍 적으로 맵을 선택했을때, 경로에 따라 갈 수 있는 다음 구간이 나눠지도록 하는 작업을 상속을 통해서 구현하고 싶었지만 실패해서, UGUI로 마무리 해놓은 상태로 리팩토링 할 때 다시 도전해볼 생각입니다.
기획적으로는 아직 구현하지 못한 **상점**과 **유물** 구현이 미완성으로 남아 아쉽습니다.

**임건형**





