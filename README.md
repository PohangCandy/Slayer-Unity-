## 목차

[개요](#개요)   
[게임설명](#게임설명)  
[팀원소개](#팀원소개)   
[역할분담](#역할분담)   
[플레이방식](#플레이방식)  
[프로젝트 후기](#프로젝트-후기)  
[개선점](#개선점)


## 개요 

-   프로젝트 이름: DataHunter :video_game:
-   프로젝트 지속기간: 2023.01-2023.02
-   개발 엔진 및 언어: Unity & C#
-   멤버: **SweetCone**, :art: ( 안혜지, 차규빈 ), :computer: ( 임건형, 민기찬)

## 게임설명 
**“가상세계 위협하는 바이러스, 과연 인터넷을 되찾을 수 있을까?”**
- 사이버 펑크 미래에서 진행되는 **CCG**(Collectible Card Game) Game

![UI_타이틀](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/94968c3f-b932-4e2e-986c-5e8548e5f8d6)
![_2024_06_10_21_09_06_990-ezgif com-video-to-gif-converter (1)](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/adad940e-9366-4934-b935-5b0889e43e33)

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

## 역할분담 
### 그래픽
**안혜지**: 게임 기획 메인, 캐릭터,배경 서브

**차규빈**: 캐릭터,배경 메인, 게임 기획 서브, UI,UX

### 소프트웨어
![header](https://capsule-render.vercel.app/api?type=slice&text=민기찬&color=2BAE66FF&fontColor=FCF6F5FF&fontAlign=90&fontAlignY=40&fontSize=50)
#### 시스템 기획
##### Figma로 UI기획
[UI기획서.pptx](https://github.com/user-attachments/files/15782024/UI.pptx)

![_2024_06_10_22_02_08_820-ezgif com-video-to-gif-converter (1)](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/6118f714-8bc2-4b98-b4d4-1df8df6c0e05)

##### UGUI로 UI배치

타이틀|맵|인게임
----|----|----|
![UI_타이틀](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/746ea3b6-2293-449e-872c-3fcb4afefcd8)|![UI_맵](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/670333ab-ab32-4be2-bee0-3dae0175500a)|![UI_플레이_아이콘](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/90a36e25-2d10-4bf0-bf6d-71252f4086e0)|

#### 몬스터 전체 구현
##### 주요기능
버프 계산|다음상태 예고
----|----|
![적 버프계산](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/0843542f-1bad-4b44-b7e2-c01847004834)|![적 다음 행동 예고](https://github.com/PohangCandy/Slayer-Unity-/assets/130345776/2a3cc55f-f147-4f42-a4a3-c132b760d01f)|
[@pseudo-code](https://www.notion.so/b865431b937f43508232babfdf1f3db7?pvs=4)|[@pseudo-code](https://www.notion.so/ba29f0d1b37146f6b89336edc0b2c6a6?pvs=4)|


![header](https://capsule-render.vercel.app/api?type=slice&text=임건형&color=755139FF&fontColor=F2EDD7FF&fontAlign=90&fontAlignY=40&fontSize=50)
#### 데이터 기획

#### 카드 구현

#### 포션 구현

#### 플레이어 구현

## 플레이방식
보스의 방을 향해 나아가며 카드와 아이템을 수집하고 전략을 세워 적을 물리치는 게임


## 프로젝트 후기
### 그래픽
#### 안혜지

#### 차규빈


### 소프트웨어
#### 민기찬
갑작스러운 일정이었음에도 끝까지 열심히 참여해준 팀원 분들께 너무 감사하고, 지난번 프로젝트때 같이 작업 했던 분들과 함께해서 빠른 시간안에 좋은 결과물이 나온 것 같아 좋았습니다.

지난번 작품과 달리 **성능 향상**과 **최적화**에 신경을 쓰기위해서, **Update문**을 사용하지 않고 적의 **FSM**을 구현함으로써 현재 행동과 다음 행동 예고에 대한 로직을 세워보았습니다. 특히 적이 시작한 버프의 경우 다시 적의 턴이 되었을 때 1턴의 효과를 지나고, 플레이어가 건 디버프의 경우 다시 플레이어의 턴이 되었을 때 1턴이 지나는 기능을 매프레임 단위로 적의 버프, 디버프를 체크하지 않고 구현하는 작업이 상당히 까다로웠으나 턴제 게임이기에 가능한 턴 종료 버튼을 누렀을 때의 확실한 턴 전환이 있닫는 점을 생각해서 **턴 계산 방법**을 생각해낸 것이 개인적으로 뿌듯했습니다.

#### 임건형

## 개선점
### 그래픽
**안혜지**

**차규빈**


### 소프트웨어
**민기찬**
프로그래밍 적으로 맵을 선택했을때, 경로에 따라 갈 수 있는 다음 구간이 나눠지도록 하는 작업을 상속을 통해서 구현하고 싶었지만 실패해서, UGUI로 마무리 해놓은 상태로 리팩토링 할 때 다시 도전해볼 생각입니다.
기획적으로는 아직 구현하지 못한 **상점**과 **유물** 구현이 미완성으로 남아 아쉽습니다.

**임건형**





