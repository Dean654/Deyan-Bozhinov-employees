import Upload from "../common/Upload";
import ProjectsList from "../Projects/Projects";
import useManageProjects from "../hooks/use-projects";

const ProjectsPage = () => {
  const manageProjects = useManageProjects();
  const onFileUploadHandler = (obj) => {
    if (obj.error) {
      manageProjects.setProjects([]);
      return;
    }

    manageProjects.uploadFile(obj.file)
  };

  return (
    <>
      <Upload uploadFile={onFileUploadHandler} allowedExtension="csv" />
      <ProjectsList projects={manageProjects.projects} />
    </>
  );
};

export default ProjectsPage;
