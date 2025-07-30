# Unity Game Project

このUnityプロジェクトはGitHub Pagesでデプロイされています。

## 🎮 プレイ

ゲームは以下のURLでプレイできます：
https://[your-username].github.io/[repository-name]/

## 🚀 デプロイ方法

### 方法1: 既存のビルドを使用（推奨）

1. `web/` ディレクトリに既にWebGLビルドが含まれています
2. このリポジトリをGitHubにプッシュします
3. GitHubの設定で「Pages」を有効にし、ソースを「GitHub Actions」に設定します
4. `web`ブランチにプッシュすると自動的にデプロイされます

### 方法2: 新しいビルドを作成

1. Unityでプロジェクトを開く
2. File > Build Settings で WebGL プラットフォームを選択
3. Build を実行し、`web/` ディレクトリに出力
4. `web`ブランチに変更をコミットしてプッシュ

## 📁 プロジェクト構造

```
├── Assets/          # Unityアセット
├── web/            # WebGLビルドファイル
│   ├── Build/      # ゲームファイル
│   ├── TemplateData/ # テンプレートデータ
│   └── index.html  # メインHTMLファイル
└── .github/workflows/ # GitHub Actions設定
```

## 🔧 必要な設定

### GitHub Secrets（方法2を使用する場合）

- `UNITY_LICENSE`: Unityライセンス（有料版を使用している場合）

### GitHub Pages設定

1. リポジトリの Settings > Pages に移動
2. Source を "GitHub Actions" に設定
3. デプロイが完了すると、URLが表示されます

## 📝 注意事項

- WebGLビルドは大きなファイルサイズになる場合があります
- 初回デプロイには数分かかる場合があります
- ブラウザのWebGL対応が必要です
