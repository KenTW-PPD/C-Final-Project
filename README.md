# Unity C# 期末專題     
Unity這是一個基於 Unity 引擎開發的  **類吸血鬼（Vampire Survivors-lik**  動作生存遊戲。      
玩家必須在無盡的怪物包圍中生存，並透過升級與寶箱強化自身實力。

## 遊戲簡介
在黑暗的荒野中，你將面對從四面八方湧入的怪物軍團。    
作為唯一的倖存者，你需要透過靈活的走位與自動化的武器系統，擊敗怪物、拾取經驗球，並在最終與強大的 BOSS 展開決戰。

- 遊戲特色動態生成系統：
  - 怪物會隨著遊戲時間增加強度與數量。
  - 自動戰鬥機制：專注於走位與策略選擇。
  - 隨機寶箱獎勵：擊敗敵人後機率掉落寶箱。

## 開發環境與安裝指南
- 開發環境Unity Version: 6000.2.15f1    
- Language: C#    
- Platform: Windows    
- 安裝步驟
  - 複製專案：    
  ` git clone https://github.com/KenTW-PPD/C-Final-Project.git `    
  或是直接下載資料夾    
  - 開啟專案：    
    - 打開 Unity Hub。    
    - 點擊 Add -> Add project from disk。    
    - 選擇下載後的原始碼資料夾。    
  - 編譯與執行：
    - 在 Project 視窗中找到 Scenes 資料夾，打開 SimpleStartScene。    
    - 點擊 Unity 頂部的 Play 按鈕即可開始測試。    
    - 若要輸出執行檔：File -> Build Settings -> Build    

## 操作說明
本遊戲設計為簡約的單手操作邏輯，讓玩家能更專注於策略躲避。    
動作按鍵說明移動 W A S D 控制角色在場景中全方位移動攻擊自動執行角色會自動鎖定並攻擊最近的敵人。

## AI工具協作    
此專題有使用AI輔助程式撰寫，主要是用於部分功能出錯時的詢問與判斷，如遊戲介面問題。    
