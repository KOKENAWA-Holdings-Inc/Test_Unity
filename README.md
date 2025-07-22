# Unity WebGL Game

このプロジェクトはUnityで作成されたWebGLゲームです。

## GitHub Pages デプロイ

このプロジェクトはGitHub Actionsを使用してGitHub Pagesに自動デプロイされます。

### セットアップ手順

1. **GitHub Pagesを有効にする**
   - リポジトリのSettings → Pages
   - Source: "Deploy from a branch" を選択
   - Branch: `gh-pages` を選択
   - Save をクリック

2. **Unity License（必要な場合）**
   - リポジトリのSettings → Secrets and variables → Actions
   - `UNITY_LICENSE` という名前でUnityライセンスを追加（Unity Proを使用している場合）

3. **プッシュしてデプロイ**
   - mainブランチまたはmasterブランチにプッシュすると自動的にデプロイされます

### ワークフロー

- **deploy-existing-webgl.yml**: 既存のWebGLビルド（web/ディレクトリ）を直接デプロイ

### アクセス

デプロイ後、以下のURLでアクセスできます：
`https://[ユーザー名].github.io/[リポジトリ名]/`

## 開発

- Unity 2022.3以降で開発
- WebGLプラットフォームでビルド
- ブラウザで動作するゲーム
