import styles from "./Projects.module.css";
import ProjectDetails from "./ProjectDetails";

const Projects = (props) => {
  const { projects } = props;

  return (
    <div className={styles.table}>
      <h1 id="tableLabel">Employees:</h1>
      {projects.length === 0 && <p>Please upload file !</p>}
      {projects.length !== 0 && (
        <table className="table table-striped" aria-labelledby="tableLabel">
          <thead>
            <tr>
              <th>Employee ID #1</th>
              <th>Employee ID #2</th>
              <th>Project ID</th>
              <th>Days worked</th>
            </tr>
          </thead>
          <tbody>
            {projects.map((project) => (
              <ProjectDetails key={project.projectId} project={project} />
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Projects;
