# Version Control
The purpose of this document is to establish how version control will be used within the project.

## Table of Contents
- [Pushing Changes](#pushing-changes)
- [Branching](#branching)
  - [Main Branch](#main-branch)
  - [Feature Branches](#feature-branches)
  - [Bug Fix Branches](#bug-fix-branches)
  - [Release Branches](#release-branches)
  - [Hotfix Branches](#hotfix-branches)
  - [Integration Branches](#integration-branches)
  - [Experimental Branches](#experimental-branches)
  - [Documentation Branches](#documentation-branches)
  - [Testing Branches](#testing-branches)
  - [Refactoring Branches](#refactoring-branches)
  - [UI/UX Branches](#ui-ux-branches)
  - [Asset Branches](#asset-branches)
  - [Localization Branches](#localization-branches)
- [Commit Messages](#commit-messages)
  - [Additions](#additions)
  - [Updates](#updates)
  - [Fixes](#fixes)
  - [Improvements](#improvements)
  - [Optimizations](#optimizations)
  - [Bug Reports](#bug-reports)
  - [Documentation](#documentation)
  - [Dependencies](#dependencies)
  - [Testing](#testing)
  - [Refactoring](#refactoring)
  - [Branch Management](#branch-management)
  - [Reverts](#reverts)
- [Tags](#tags)
  - [Semantic Versioning](#semantic-versioning)
- [Conflict Resolution](#conflict-resolution)


## Pushing Changes 

- **Use Descriptive Branch Names:** When creating a new branch for your feature or fix, give it a descriptive name that conveys its purpose. For example, "feature/user-authentication."

- **Frequent Commits:** Make frequent and meaningful commits to your branch. Each commit should represent a logical and self-contained change. This makes it easier to review and understand the changes.

- **Branch Cleanup:** Regularly clean up your branch by removing unnecessary or outdated commits. You can use interactive rebasing to squash, edit, or reorder commits before pushing.

- **Write Meaningful Commit Messages:** Provide clear and concise commit messages that explain what each change does. Use imperative language (e.g., "Add user authentication feature" instead of "Added user authentication feature").

- **Push Frequently:** Push your local branch to the remote repository regularly to keep it up-to-date. This ensures that your work is accessible to other team members and avoids potential merge conflicts.

- **Approval Workflow:** Developers should not merge their work directly into the main branch. Instead, they should create a pull request (PR) and assign it to the lead developer for review.

- **Use a Clear Title and Description:** When creating a PR, use a descriptive title that summarizes the change. In the PR description, provide context about what the change accomplishes and why it's necessary.

- **Approval:** Once reviewers are satisfied with the changes, they can approve the PR. Some projects may require multiple approvals before merging.

## Branching

### Main Branch
The primary development branch where the stable and production-ready code resides. It should always reflect the latest released version of your project.

---

### Feature Branches
 - These branches are used for developing new features or adding significant functionality to the project. Feature branches allow developers to work on new code without affecting the main branch.

 **Naming Convention:**
- feature/[feature]

---

### Bug Fix Branches
 - These branches are created to address specific bugs or issues reported in the main branch. Bug fix branches allow developers to work on resolving issues without interfering with ongoing development.

 **Naming Convention:**
- bugfix/[fix]

---

### Release Branches
 - When you're preparing for a new release or version of your project, you can create release branches. These branches are used for finalizing and testing the code before deployment.

 **Naming Convention:**
- release/[version]

---

### Hotfix Branches
 - In case critical issues or bugs need immediate attention, hotfix branches are created to fix them. Hotfixes are typically applied to the main branch and other relevant release branches.

 **Naming Convention:**
- hotfix/[fix]

---

### Integration Branches
 - For collaborative development, you might create integration branches where team members merge their feature branches to test and integrate their work together before merging it into the main branch.

 **Naming Convention:**
- integration/[feature1]-[feature2]

---

### Experimental Branches
 - Sometimes, you may want to experiment with new ideas, technologies, or major changes that are not yet ready for the main branch. Experimental branches are used for such experimentation.

 **Naming Convention:**
- experimental/[feature]

---

### Documentation Branches
 - If you're maintaining project documentation or technical documentation alongside your Unity project, you can create documentation branches to manage changes to these documents.

 **Naming Convention:**
- documentation/[feature]

---

### Testing Branches
 - These branches are used for testing specific aspects of the project, such as performance testing, compatibility testing on different platforms, or unit testing.

 **Naming Convention:**
- testing/[version]

---

### Refactoring Branches
 - When you're planning substantial code refactoring or restructuring of your project, creating refactoring branches can help separate these major changes from regular development.

**Naming Convention:**
- refactor/[feature]

---

### UI UX Branches
 - For improvements related to user interface (UI) and user experience (UX), such as redesigning menus or improving user interactions, dedicated branches can be created.

 **Naming Convention:**
- uiux/[feature/aspect]

---

### Asset Branches
 - If you need to make significant changes to project assets or reorganize asset folders, you can create branches specifically for asset-related work.

 **Naming Convention:**
- assets/[asset type]

---

### Localization Branches
 - If you're working on adding or updating translations and localization support for your project, localization branches can be used to manage this work.

 **Naming Convention:**
- localization/[language]


## Commit Messages

### Additions
For adding new features, assets, or functionality to the project. For example, adding new character models, animations, or new game features.

#### Examples
- Add new character model and animations
- Integrate particle effects for enemy spawn
- Implement a settings menu for audio control

---

### Updates
For making updates or improvements to existing features or assets. This can include UI improvements, code enhancements, or level design updates.

#### Examples
- Update UI layout for better mobile compatibility
- Update character movement script for smoother animation transitions
- Update level design based on feedback

---

### Fixes
For addressing and fixing bugs or issues in the project. This can encompass fixing gameplay-related issues, UI problems, or technical glitches.

#### Examples
- Fix character collider bug causing player to fall through the ground
- Fix enemy AI pathfinding issue
- Fix UI button not responding to clicks

---

### Improvements
For enhancing the overall quality or user experience of the project. This can involve graphical improvements, gameplay enhancements, or code optimizations.

#### Examples
- Improve lighting and shadows in level 3
- Improve player jump mechanics for better gameplay experience
- Improve code readability by refactoring enemy spawning logic

---

### Optimizations
For optimizing the project for better performance, such as improving rendering, reducing load times, or enhancing memory management.

#### Examples
- Optimize rendering pipeline for better performance
- Optimize resource loading to reduce load times
- Optimize memory usage in audio manager

---

### Bug Reports
For addressing issues reported by users or team members. Mention the specific issue numbers or descriptions you're resolving.

#### Examples
- Address issue #432: Resolve crashing bug on startup
- Fix issue #567: Handle null reference error in player script
- Close issue #789: Improve network synchronization

---

### Documentation
For making changes or additions to project documentation. This can include updating README files, documenting APIs, or adding code comments for clarity.

#### Examples
- Update README with instructions for setting up the project
- Document public API for custom scripts
- Add inline code comments for clarity

---

### Dependencies
For managing or updating project dependencies, such as Unity version updates or package changes.

#### Examples
- Update Unity version to 2020.3.2f1
- Update TextMeshPro package to version 3.0.0
- Add PostProcessing v2.0.1 package for improved visual effects

---

### Testing
For conducting testing activities, including adding tests, confirming compatibility on different platforms, or assessing performance.

#### Examples
- Add unit tests for the player health system
- Test game functionality on macOS and confirm compatibility
- Performance testing for level loading times

---

### Refactoring
For restructuring or improving the codebase for better readability and maintainability. This can involve renaming variables, methods, or splitting large scripts into smaller components.

#### Examples
- Refactor character movement script for readability and modularity
- Rename variables and methods for consistency
- Split a monolithic script into smaller reusable components

---

### Branch Management
For handling version control tasks, like merging branches, resolving conflicts, or creating new branches for features.

#### Examples
- Merge feature/character-customization into develop
- Resolve conflicts in feature/enemy-ai branch
- Create feature/level-selection branch

---

### Reverts
For reverting changes made in previous commits due to unintended issues or problems that need to be addressed.

#### Examples
- Revert commit abc123: Reverting changes due to unintended issues
- Revert feature/enhancement-X: Not ready for production

## Tags
Tags will be mainly used to label builds with their corresponding versions

### Semantic Versioning 
SemVer uses three numbers separated by dots, like "MAJOR.MINOR.PATCH." Each number has a specific meaning:
- **MAJOR:** Increment this number for significant changes that might break compatibility, such as major feature additions or breaking changes.
- **MINOR:** Increment this number for smaller, backward-compatible additions or enhancements to existing features.
- **PATCH:** Increment this number for bug fixes or small, backward-compatible changes.

## Conflict Resolution
Conflict resolution is the process of handling discrepancies and conflicts that arise when multiple team members make changes to a shared codebase. These conflicts can occur when two or more developers attempt to modify the same part of a file simultaneously or when changes made in separate branches need to be merged.

- **Use Branches**: Encourage team members to work on separate branches, each dedicated to a specific task or feature. This reduces the likelihood of conflicts when merging changes into the main branch.

- **Regular Commits**: Encourage developers to make frequent, smaller commits rather than infrequent, larger ones. This makes it easier to pinpoint when and where conflicts occurred.

- **Pull and Fetch**: Regularly pull and fetch changes from the main branch to stay up to date with the latest code. This reduces the chances of conflicts by ensuring you're working with the most recent codebase.

- **Conflict Resolution Tools**: Familiarize the team with the conflict resolution tools provided by your version control system (e.g., Git). Developers should know how to resolve merge conflicts and utilize these tools effectively.

- **Conflict Resolution Policy**: Develop and communicate a conflict resolution policy within the team. Outline the steps to take when conflicts arise and the responsible parties for resolving them.