# Phonics Island 卡皮巴拉自然發音大冒險

給國小低年級和幼兒園孩子玩的 3D 英文自然發音遊戲（Unity，Windows）。
好人卡皮巴拉游到六座小島，每座島學一種發音，答對就丟柚子打壞卡皮巴拉；
打贏後壞卡皮巴拉會變成好朋友，去溫泉泡湯。

## 小島與學習內容

| 小島 | 學什麼 |
|---|---|
| 字母森林 | 短母音 a e i o u |
| 組合音洞穴 | sh ch th wh、子音混合音（fl fr tr cr st sn…） |
| 單字海灘 | ee ea ai ay，在沙灘上走動找出聽到的東西 |
| 拼字山谷 | oo（moon / book）、ow ou oi oy、看圖拼字 |
| 句型村莊 | I see / I like / I have / Where is…、-s / -es 複數 |
| 怪物城堡 | 全部混合複習，最後大魔王 |

- 約 170 個單字，每次玩都隨機出題。
- 用滑鼠點就能玩（點小島自動游過去、點朋友自動走過去），也可以用方向鍵。
- 家長設定：弟弟模式（圖片、單字）／姊姊模式（句子、拼字）、中文提示開關、全部開放、重設進度。
- 語音使用 Windows 內建英文語音；答錯不扣分、不會失敗，提示會越來越明顯。

## 怎麼玩

1. 用 Unity 6000.6.7f1 開啟這個資料夾。
2. 選單 **Phonics Island > Build Windows EXE**，會產生 `Build/PhonicsIsland/PhonicsIsland.exe`。
3. 雙擊 `Play_PhonicsIsland.bat` 開始玩。

（也可以在 Unity 開啟 `Assets/Scenes/SampleScene` 直接按 Play。）

## 程式檔案

- `Assets/PhonicsIslandGame.cs`：流程、移動、鏡頭、進度存檔
- `Assets/PhonicsContent.cs`：小島與對話
- `Assets/PhonicsWordBank.cs`：單字庫與隨機出題
- `Assets/PhonicsWorld.cs`：地圖、小島場景、卡皮巴拉
- `Assets/PhonicsQuiz.cs`：題目流程、攻擊、提示
- `Assets/PhonicsAudio.cs`：語音與音效
- `Assets/PhonicsUI.cs`：畫面介面
- `Tools/make_pictures.ps1`：產生單字圖片

## 素材出處與授權

- 3D 卡皮巴拉模型："Capybara" by Poly by Google，[CC BY 3.0](https://creativecommons.org/licenses/by/3.0/)，來源 <https://poly.pizza/m/66d-mKAgF17>（遊戲中只調整顏色）
- 音效：[Kenney](https://www.kenney.nl)（Interface Sounds、Impact Sounds、Music Jingles），CC0
- 單字圖片：Microsoft Fluent Emoji 風格的表情符號，由 Windows 內建的 Segoe UI Emoji 字型繪製
- 中文字型（網頁版）：思源黑體 Noto Sans TC（Google Fonts），[SIL OFL 1.1](Assets/Resources/Fonts/OFL.txt)；只保留遊戲用到的字並改名為 "Phonics Island Font"

## 網頁版

直接在瀏覽器玩：<https://brianhu1008.github.io/PhonicIsland/>

- 雙擊 `BuildWeb.bat` 會重新打包網頁版到 `docs/`，再上傳 GitHub，網頁版 1～2 分鐘後更新。
- 網頁版用瀏覽器內建的英文語音（Chrome、Edge 的語音比較好聽）。
- 遊戲裡新增中文字後，要用 `Tools/make_font.py` 重新產生字型，網頁版才顯示得出來。
