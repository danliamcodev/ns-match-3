# Development Philosophy

The purpose of this document is to establish standards when using version control for projects.

## Table of Contents

- [Empathetic Development](#empathetic-development)
- [S.O.L.I.D Principles](#solid-principles)
  - [Single Responsibility Principle](#solid-principles)
  - [Open/Closed Principle](#solid-principles)
  - [Liskov Substitution Principle](#solid-principles)
  - [Interface Segregation Principle](#solid-principles)
  - [Dependency Inversion Principle](#dependency-inversion-principle)
- [Reusability](#reusability)
- [Scalability](#scalability)
- [Editability](#editability)
- [Communication](#communication)
- [Documentation](#documentation)
- [Use of AI](#use-of-ai)

## Empathetic Development

"Empathetic Development" refers to the practice of creating games with a deep understanding of and empathy for the players and fellow developers.

- **Player-Centered Design:** Prioritize the player's needs, desires, and feedback. Design gameplay, UI, and mechanics with the player's perspective in mind. Developers must possess both the proactiveness to anticipate the user's needs, and the humility to put the player experience above the developer's assumptions.

- **Accessibility:** Ensure that your games are accessible to a broad audience, including players with disabilities. Implement features like adjustable difficulty levels, subtitles, and alternative control schemes.

- **Inclusivity:** Promote diversity and inclusivity in character design, narratives, and game environments. Represent a variety of perspectives and cultures in your games.

- **Effective Communication:** Promote clear and open communication within the team. Encourage developers to ask questions, seek help, and share knowledge.

- **Readability** Readability focuses on making the code, assets, and game design clear and understandable. It's essential for ensuring efficient collaboration within the team and for maintaining a project over time.

## S.O.L.I.D. Principles

### **Single Responsibility Principle**

   The SRP states that a class or script should have a single responsibility, such as managing player movement or handling UI interactions.

   Example:

   ```csharp
   // Bad Example - A Unity script with multiple responsibilities
   public class PlayerController : MonoBehaviour
   {
       // Handles both player movement and shooting
       void MovePlayer() { /* ... */ }
       void Shoot() { /* ... */ }
   }

   // Good Example - Separating responsibilities into different Unity scripts
   public class PlayerMovement : MonoBehaviour
   {
       // Handles player movement
       void MovePlayer() { /* ... */ }
   }

   public class PlayerShooting : MonoBehaviour
   {
       // Handles shooting behavior
       void Shoot() { /* ... */ }
   }
   ```

### **Open/Closed Principle**

   The OCP encourages creating Unity components and systems that can be extended without modifying existing code. Code should be OPEN to extension but CLOSED to modification.

   Example:

   ```csharp
   // Bad Example - Modifying existing Unity scripts for new features
   public class Player : MonoBehaviour
   {
       // Existing code
   }

   // Extending existing Unity script
   public class Player : MonoBehaviour
   {
       // Existing code
       void NewFeature() { /* ... */ }
   }
   
   // Good Example - Creating new Unity scripts for new features
   public class Player : MonoBehaviour
   {
       // Existing code
   }

   public class NewFeature : MonoBehaviour
   {
       // New feature code
   }
   ```

### **Liskov Substitution Principle**

   The LSP states that Unity components or scripts derived from a base class should be interchangeable without affecting the behavior of the game. A child class should be able to do everything a parent class can.

   Example 1:

   ```csharp
   // Bad Example - Violating LSP
   public class Enemy : MonoBehaviour
   {
       public void Attack() { /* ... */ }
   }

   public class Boss : Enemy
   {
       public void Attack()
       {
           // Behavior specific to the boss
       }
   }

   // Good Example - Complying with LSP
   public interface IAttackable
   {
       void Attack();
   }

   public class Enemy : MonoBehaviour, IAttackable
   {
       public void Attack() { /* ... */ }
   }

   public class Boss : MonoBehaviour, IAttackable
   {
       public void Attack()
       {
           // Behavior specific to the boss
       }
   }
   ```

   Example 2:

   ```csharp
   // Bad Example - Violating LSP
public class Humanoid : MonoBehaviour
{
    public void NormalAttack()
    {
        Debug.Log("Hit with Weapon");
    }

    public virtual void AttackWithElfMagic()
    {
        Debug.Log("Do nothing");
    }

        public virtual void AttackWithOrcRage()
    {
        Debug.Log("Do nothing.");
    }
}

public class Elf : Humanoid
{
    public void NormalAttack()
    {
        Debug.Log("Hit with Weapon");
    }

    public virtual void AttackWithElfMagic()
    {
        Debug.Log("Use Elf Magic");
    }

        public virtual void AttackWithOrcRage()
    {
        Debug.Log("Do nothing.");
    }
}

public class Orc : Humanoid
{
    public void NormalAttack()
    {
        Debug.Log("Hit with Weapon");
    }

    public virtual void AttackWithElfMagic()
    {
        Debug.Log("Do nothing");
    }

        public virtual void AttackWithOrcRage()
    {
        Debug.Log("Go into Orc Rage.");
    }
}

   // Good Example - Complying with LSP
public class Humanoid : MonoBehaviour
{
    public void NormalAttack()
    {
        Debug.Log("Hit with Weapon");
    }

    public virtual void SpecialAttack()
    {
        Debug.Log("Kick away the opponent");
    }
}

public class Elf : Humanoid
{
    public void NormalAttack()
    {
        Debug.Log("Hit with Weapon");
    }

    public virtual void SpecialAttack()
    {
        Debug.Log("Use elf magic");
    }
}

public class Orc : Humanoid
{
    public void NormalAttack()
    {
        Debug.Log("Hit with Weapon");
    }

    public virtual void SpecialAttack()
    {
        Debug.Log("Go into Orc Rage.");
    }
}
   ```

### **Interface Segregation Principle**

   The ISP encourages creating small and specific Unity interfaces to avoid forcing Unity components to implement methods they don't need.

   Example:

   ```csharp
   // Bad Example
   public class Worker
   {
       void Work();
       void Eat();
   }

   // Unity components are forced to implement both Work and Eat, even if they don't need one of them

   // Good Example - Segregated Unity interfaces
   public interface IWorker
   {
       void Work();
   }

   public interface IEater
   {
       void Eat();
   }
   ```

### **Dependency Inversion Principle:**

   The DIP emphasizes high-level Unity components depending on abstractions and low-level Unity components implementing those abstractions. High-level  modules should not depend on low-level modules.

   Example:

   ```csharp
   // Bad Example - High-level Unity component directly depends on a low-level Unity component
   public class GameManager : MonoBehaviour
   {
       private PlayerController playerController; // Direct dependency
       
       void Start()
       {
           playerController = GetComponent<PlayerController>();
       }
   }

   // Good Example - Abstraction and dependency inversion in Unity
   public interface IPlayerController
   {
       void MovePlayer();
       void Shoot();
   }

   public class PlayerController : MonoBehaviour, IPlayerController
   {
       public void MovePlayer() { /* ... */ }
       public void Shoot() { /* ... */ }
   }

   public class GameManager : MonoBehaviour
   {
       private IPlayerController playerController; // Dependency on the abstraction
       
       void Start()
       {
           playerController = GetComponent<PlayerController>();
       }
   }
   //This only emphasizes on abstractions. The GameManager depending on a Player Controller is still a rigid connection that must ideally be avoided. The Player Controller script also violates the Single Responsibility Principle. A better example must be made.
   ```

## Reusability

Reusability is a key principle that focuses on maximizing efficiency and minimizing redundant work. It involves designing components and systems that can be used in multiple places within the game or across different projects.

- **Component-Based Architecture:** Create game systems and components that can be easily reused in different contexts. For example, design a versatile inventory system that can be used in various game genres.

- **Asset Packages:** Package and share reusable assets (e.g., character models, sound effects) with other team members or the Unity Asset Store.

- **Scriptable Objects:** Utilize Scriptable Objects in Unity to create data-driven assets that can be reused for various purposes, such as defining items, abilities, or dialogue trees.

- **Design Patterns:** Implement design patterns like the Object Pooling pattern to recycle and reuse game objects efficiently.

## Scalability

Scalability is about designing your game in a way that allows it to grow and adapt to changing requirements, whether that means adding more content or handling increased player loads.

- **Performance Optimization:** Optimize your code, graphics, and resource usage to ensure the game runs smoothly on a variety of hardware.

- **Flexible Systems:** Design systems that can accommodate new features and content without major overhauls. This may involve creating dynamic UIs or supporting various device resolutions.

- **Server Architecture:** If your game involves online components, design a server architecture that can handle scaling as more players join the game.

- **Content Expansion:** Plan for easy content expansion by using data-driven design, allowing for the addition of new levels, characters, or items without altering core systems.

- **Load Management:** Implement efficient loading and streaming systems to manage the loading of game assets and environments dynamically.

- **Modular Development** Games must be broken down into smaller, manageable modules or components. This approach makes it easier to both add and remove features down the development pipeline.

- **Avoiding Singletons** Singletons will be avoided as it creates rigid connections, breaking modularity and making the project less scalable.

## Editability

Editability is about designing your Unity project in a way where gameplay is easily editable in the Editor. This makes it so that new code does not necessarily need to be made to come up with new behaviors. This creates a fluid system that allows game designers to experiment and come up with new and interesting game mechanics more easily. This also reduces the amount of code needed, resulting in less bugs as well.

- **Inspector Customization:** Use the Unity Editor's custom inspector features to make your components more user-friendly. You can add tooltips, context menus, and reorderable lists to make it easier for designers to interact with your components.

- **Scriptable Objects for Tuning:** Utilize Scriptable Objects for game parameters and tuning. This allows designers to tweak values and behaviors directly in the Unity Editor without modifying code. For example, you can create a "GameSettings" Scriptable Object that stores various gameplay parameters.

- **Event Systems:** Implement event-driven systems that allow designers to define interactions and behaviors through the Unity Editor. Unity's Event System and tools like the Visual Effect Graph can help achieve this.

- **Modular Level Design:** Design levels and scenes in a modular way, using prefabs and tilesets. This allows designers to build and iterate on game levels without deep programming knowledge.

- **Data-Driven Gameplay:** Create systems that rely on external data files (e.g., JSON, XML, or CSV) for defining game content and rules. This makes it easy for designers to adjust game properties without needing to access the codebase.

- **Editor Extensions:** Develop custom editor extensions and tools that streamline the content creation process. These tools can automate repetitive tasks and provide a more efficient workflow for designers.

- **In-Editor Testing:** Enable in-Editor testing features that allow designers to playtest and iterate on gameplay directly within the Unity Editor, reducing the need to build and deploy the game frequently.

## Communication

Effective communication is the cornerstone of a successful game development project. It ensures that team members can collaborate seamlessly, share ideas, and resolve issues efficiently.

- **Team Collaboration:** Encourage a collaborative atmosphere where team members can freely share ideas, feedback, and suggestions. Foster an environment where everyone's voice is heard and valued.

- **Project Management Tools:** Utilize project management and communication tools such as Trello, JIRA, or Slack to streamline communication. These tools help track tasks, deadlines, and project progress.

- **Feedback Loops:** Establish feedback loops for code reviews, design reviews, and playtesting. These loops facilitate the exchange of constructive feedback, helping team members improve their work.

- **Transparency:** Maintain transparency regarding project goals, milestones, and challenges. Share project timelines, design documents, and roadmaps to ensure that everyone has a clear understanding of the project's direction.

## Documentation

Effective documentation is crucial for maintaining clarity, consistency, and alignment within the game development process.

- **Project Structure:** Document the organization of the game project, including folder structures, naming conventions, and the purpose of each directory. This provides an overview of where to find specific assets and code components.

- **Technical Stack:** Describe the technical stack used for the project, including programming languages, game engines, libraries, and tools. This information helps team members understand the technology behind the game.

- **Coding Standards:** Establish and document coding standards and best practices that team members should follow. This ensures code consistency and readability throughout the project.

- **API Documentation:** If the project includes custom scripts or systems, provide clear and up-to-date API documentation. This documentation should describe the purpose, usage, and parameters of functions and classes.

- **Testing Documentation:** Detail the testing approach, including test cases, test plans, and bug reporting processes. This ensures that the testing phase is well-structured.

- **Knowledge Base:** Create a knowledge base that houses tutorials, guides, and FAQs related to the project. This resource assists team members in acquiring the necessary skills and knowledge to work effectively on the project.

- **Onboarding Guides:** Develop onboarding guides or materials for new team members to quickly get up to speed with the project's goals, codebase, and development processes.

- **Troubleshooting:** Create a troubleshooting guide that addresses common technical issues or errors that team members may encounter during development.

- **Version Control Practices:** Outline version control practices and guidelines for team members to follow when using version control systems like Git. This ensures smooth collaboration and code management.

## Use of AI

AI can be utilized in the development process to help create robust code and to help bugfix or troubleshoot any issues. On the topic of AI art, AI can be used to create reference images at most. AI must be avoided when creating any art that will make it in the final product as there is a chance that stolen art was used to create the AI art.
