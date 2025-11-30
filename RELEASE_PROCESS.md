# Release Process

This repository uses GitHub Actions to automatically create releases.

## How to Create a New Release

1. **Create a version branch** with the version number:
   ```bash
   git checkout -b v0.1.1
   git push origin v0.1.1
   ```

2. **GitHub Actions will automatically:**
   - Extract the version from the branch name (e.g., `v0.1.1`)
   - Update the version in `WinterForWindows.csproj`
   - Build self-contained executables for:
     - Windows x64 (Intel/AMD processors)
     - Windows ARM64 (Surface devices, etc.)
   - Create portable .zip versions
   - Generate release notes
   - Create a GitHub Release with tag `v0.1.1`
   - Upload all executables and zip files
   - Delete the version branch after successful release

3. **The release will be live** at:
   `https://github.com/tomorgan/winter-for-windows/releases`

## Version Branch Format

Branch names **must** start with `v` followed by the version number:
- ✅ `v0.1.0`
- ✅ `v0.2.0`
- ✅ `v1.0.0`
- ✅ `v1.2.3-beta`
- ❌ `0.1.0` (missing 'v')
- ❌ `release-0.1.0` (doesn't start with 'v')

## What Gets Created

For version `v0.1.0`, the following files are automatically created and uploaded:

1. `WinterForWindows_win-x64_v0.1.0.exe` - Single-file executable for Intel/AMD
2. `WinterForWindows_win-arm64_v0.1.0.exe` - Single-file executable for ARM
3. `WinterForWindows_win-x64_v0.1.0.zip` - Portable version for Intel/AMD
4. `WinterForWindows_win-arm64_v0.1.0.zip` - Portable version for ARM

## Release Notes

Release notes are automatically generated with:
- Feature descriptions
- Installation instructions for both architectures
- Usage guide
- Credits

## Example Workflow

```bash
# Start from main branch
git checkout main
git pull

# Create and push version branch
git checkout -b v0.1.1
git push origin v0.1.1

# GitHub Actions takes over and creates the release
# Branch is automatically deleted after successful release
```

## Monitoring the Release

1. Go to the **Actions** tab in GitHub
2. Click on the running workflow
3. Watch the build progress
4. Once complete, check the **Releases** page

## Troubleshooting

If the workflow fails:
- Check the Actions log for error messages
- Fix the issue in main branch
- Delete the failed tag: `git push origin --delete v0.1.0`
- Try again with a new version branch
